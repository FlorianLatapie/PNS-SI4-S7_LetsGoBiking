using System;
using System.Collections.Generic;

namespace ProxyServer
{
    public class Availabilities
    {
        public int bikes { get; set; }
        public int stands { get; set; }
        public int mechanicalBikes { get; set; }
        public int electricalBikes { get; set; }
        public int electricalInternalBatteryBikes { get; set; }
        public int electricalRemovableBatteryBikes { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(bikes)}: {bikes}, {nameof(stands)}: {stands}, {nameof(mechanicalBikes)}: {mechanicalBikes}, {nameof(electricalBikes)}: {electricalBikes}, {nameof(electricalInternalBatteryBikes)}: {electricalInternalBatteryBikes}, {nameof(electricalRemovableBatteryBikes)}: {electricalRemovableBatteryBikes}";
        }
    }

    public class MainStands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }

        public override string ToString()
        {
            return $"{nameof(availabilities)}: {availabilities}, {nameof(capacity)}: {capacity}";
        }
    }

    public class Position
    {
        public double latitude { get; set; }
        public double longitude { get; set; }

        public override string ToString()
        {
            return $"{nameof(latitude)}: {latitude}, {nameof(longitude)}: {longitude}";
        }
    }

    public class Station
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public Position position { get; set; }
        public bool banking { get; set; }
        public bool bonus { get; set; }
        public string status { get; set; }
        public DateTime lastUpdate { get; set; }
        public bool connected { get; set; }
        public bool overflow { get; set; }
        public object shape { get; set; }
        public TotalStands totalStands { get; set; }
        public MainStands mainStands { get; set; }
        public object overflowStands { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(number)}: {number}, {nameof(contractName)}: {contractName}, {nameof(name)}: {name}, {nameof(address)}: {address}, {nameof(position)}: {position}, {nameof(banking)}: {banking}, {nameof(bonus)}: {bonus}, {nameof(status)}: {status}, {nameof(lastUpdate)}: {lastUpdate}, {nameof(connected)}: {connected}, {nameof(overflow)}: {overflow}, {nameof(shape)}: {shape}, {nameof(totalStands)}: {totalStands}, {nameof(mainStands)}: {mainStands}, {nameof(overflowStands)}: {overflowStands}";
        }
    }

    public class TotalStands
    {
        public Availabilities availabilities { get; set; }
        public int capacity { get; set; }

        public override string ToString()
        {
            return $"{nameof(availabilities)}: {availabilities}, {nameof(capacity)}: {capacity}";
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Contract
    {
        public string name { get; set; }
        public string commercial_name { get; set; }
        public List<string> cities { get; set; }
        public string country_code { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(name)}: {name}, {nameof(commercial_name)}: {commercial_name}, {nameof(cities)}: {cities}, {nameof(country_code)}: {country_code}";
        }
    }
}