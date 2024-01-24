using TrafficCom.V3.Connection;
using TrafficCom.V3.Request;

namespace TrafficCom.V3.Feature
{
    public class DeviceClient
    {
        private readonly V3Client _client;

        public DeviceClient(V3Client client)
        {
            _client = client;
        }

        public async Task Reboot(byte reset1, byte reset2)
        {
            await _client.SendAsync(new V3RequestX0FX10
            {
                Reset1 = reset1,
                Reset2 = reset2,
            });
        }

        public async Task ResetConnection()
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX91>(new V3RequestX0FX11 { });
        }

        public async Task SetHardwareStatusReportInterval(V3HardwareReportInterval interval)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestX0FX14
            {
                Interval = interval
            });
        }

        public async Task<V3HardwareReportInterval> GetHardwareStatusReportInterval()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC4>(new V3RequestX0FX44 { });
            return res.Interval;
        }

        public async Task<ushort> GetHardwareStatus()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC1>(new V3RequestX0FX41 { });
            return res.HardwareStatus;
        }

        public async Task<byte> SetDeviceTime(DateTime? time = null)
        {
            if (!time.HasValue) time = DateTime.Now;

            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FX92>(new V3RequestX0FX12
            {
                Time = time.Value,
                Week = time.Value.DayOfWeek
            });

            return res.Diff;
        }

        public async Task<DateTime> GetDeviceTime()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC2>(new V3RequestX0FX42 { });
            return res.Time;
        }

        public async Task<Dictionary<byte, ushort>> GetAllEquipmentIds()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC0>(new V3RequestX0FX40
            {
                SearchAllEquipment = true,
            });

            return res.EquipmentIds;
        }

        public async Task SetCommandLevel(V3CommandLevel level)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestX0FX13
            {
                CommandLevel = level,
            });
        }

        public async Task<EquipmentFirmwareVersion> GetFirmwareVersion()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC3>(new V3RequestX0FX43 { });
            return new EquipmentFirmwareVersion
            {
                CommandLevel = res.CommandLevel,
                CompanyId = res.CompanyId,
                Time = res.Time,
                Version = res.Version,
            };
        }

        public async Task SetPassword(byte[] password)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestX0FX15
            {
                Password = password,
            });
        }

        public async Task<byte[]> GetPassword()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC5>(new V3RequestX0FX45 { });
            return res.Password;
        }

        public async Task SetDeviceDatabaseLock(V3DbLock dbLock)
        {
            await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestX0FX16
            {
                LockDb = dbLock
            });
        }

        public async Task<V3DbLock> GetDeviceDatabaseLock()
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC6>(new V3RequestX0FX46 { });
            return res.LockDb;
        }

        public async Task<bool> IsProtocolSupported(ushort protocol)
        {
            var res = await _client.SendAndWaitForReplyAsync<V3RequestX0FXC7>(new V3RequestX0FX47
            {
                Protocol = protocol
            });

            return res.IsSupported;
        }
    }

    public class EquipmentFirmwareVersion
    {
        public DateTime Time { get; set; }

        public byte CompanyId { get; set; }

        public byte Version { get; set; }

        public V3CommandLevel CommandLevel { get; set; }
    }
}