using System.Net.NetworkInformation;
using System.Text;

namespace CodeExercise.Utilities;

/// <summary>
/// Distributed Sequence Generator
/// Inspired by Twitter snowflake: https://github.com/twitter/snowflake/tree/snowflake-2010 and from https://www.callicoder.com/distributed-unique-id-sequence-number-generator/
/// This class should be used as a Singleton.
/// </summary>
public class SnowflakeGenerator 
{
    private const int NodeIdBits = 10;
    private const int SequenceBits = 12;

    private static readonly int MaxNodeId = (int)(Math.Pow(2, NodeIdBits) - 1);
    private static readonly int MaxSequence = (int)(Math.Pow(2, SequenceBits) - 1);

    // Custom Epoch (January 1, 2015 Midnight UTC = 2015-01-01T00:00:00Z)
    private readonly long customEpoch;
    private readonly int nodeId;

    private long lastTimestamp = -1L;
    private long sequence = 0L;

    /// <summary>
    /// Create a SequenceGenerator with a node ID
    /// </summary>
    /// <param name="nodeId"></param>
    /// <param name="epochYear"></param>
    /// <exception cref="ArgumentException"></exception>
    public SnowflakeGenerator(int nodeId, int epochYear = 2015)
    {
        if (nodeId < 0 || nodeId > MaxNodeId)
        {
            throw new ArgumentException($"NodeId must be between 0 and {MaxNodeId}");
        }

        this.nodeId = nodeId;
        customEpoch = GetUnixTimestamp(new DateTime(epochYear, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }

    /// <summary>
    /// Create a SequenceGenerator with a generated node ID 
    /// </summary>
    /// <param name="epochYear"></param>
    public SnowflakeGenerator(int epochYear = 2015) : this(CreateNodeId(), epochYear)
    {
    }

    public long GetNextId()
    {
        long currentTimestamp = GetTimestamp();

        if (currentTimestamp < lastTimestamp)
        {
            throw new InvalidOperationException("Invalid System Clock!");
        }

        if (currentTimestamp == lastTimestamp)
        {
            sequence = (sequence + 1) & MaxSequence;
            if (sequence == 0)
            {
                // Sequence Exhausted, wait till next millisecond.
                currentTimestamp = WaitNextMillis(currentTimestamp);
            }
        }
        else
        {
            // reset sequence to start with zero for the next millisecond
            sequence = 0;
        }

        lastTimestamp = currentTimestamp;

        long id = currentTimestamp << (NodeIdBits + SequenceBits);
        id |= (uint)(nodeId << SequenceBits);
        id |= sequence;
        return id;
    }


    // Get current GetTimestamp in milliseconds, adjust for the custom epoch.
    private long GetTimestamp()
    {
        return GetUnixTimestamp(DateTime.UtcNow) - customEpoch;
    }

    static long GetUnixTimestamp(DateTime time)
    {
        return (long)time.Subtract(
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }

    // Block and wait till next millisecond
    private long WaitNextMillis(long currentTimestamp)
    {
        while (currentTimestamp == lastTimestamp)
        {
            currentTimestamp = GetTimestamp();
        }

        return currentTimestamp;
    }

    private static int CreateNodeId()
    {
        int nodeId;
        try
        {
            var sb = new StringBuilder();
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var networkInterface in networkInterfaces)
            {
                sb.Append(networkInterface.GetPhysicalAddress());
            }

            nodeId = sb.ToString().GetHashCode();
        }
        catch
        {
            nodeId = (new SecureRandom().Next());
        }

        nodeId &= MaxNodeId;
        return nodeId;
    }


    public static SnowflakeGenerator Current { get; } = new SnowflakeGenerator();
}