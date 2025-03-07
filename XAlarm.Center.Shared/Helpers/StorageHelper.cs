namespace XAlarm.Center.Shared.Helpers;

public static class StorageHelper
{
    public static double Convert(Differential sizeDifferential, double unitSize,
        StorageBase baseUnit = StorageBase.Base10)
    {
        if (unitSize < 0.000000000001) return 0;

        double power1 = 1000;
        double power2 = 1000000;
        double power3 = 1000000000;
        double power4 = 1000000000000;

        if (baseUnit != StorageBase.Base2)
            return sizeDifferential switch
            {
                Differential.ByteToKilo => unitSize / power1,
                Differential.ByteToMega => unitSize / power2,
                Differential.ByteToGiga => unitSize / power3,
                Differential.ByteToTera => unitSize / power4,
                Differential.KiloToByte => unitSize * power1,
                Differential.KiloToMega => unitSize / power1,
                Differential.KiloToGiga => unitSize / power2,
                Differential.KiloToTera => unitSize / power3,
                Differential.MegaToByte => unitSize * power2,
                Differential.MegaToKilo => unitSize * power1,
                Differential.MegaToGiga => unitSize / power1,
                Differential.MegaToTera => unitSize / power2,
                Differential.GigaToByte => unitSize * power3,
                Differential.GigaToKilo => unitSize * power2,
                Differential.GigaToMega => unitSize * power1,
                Differential.GigaToTerra => unitSize / power1,
                Differential.TeraToByte => unitSize * power4,
                Differential.TeraToKilo => unitSize * power3,
                Differential.TeraToMega => unitSize * power2,
                Differential.TeraToGiga => unitSize * power1,
                _ => 0
            };
        power1 = 1024;
        power2 = 1048576;
        power3 = 1073741824;
        power4 = 1099511627776;

        return sizeDifferential switch
        {
            Differential.ByteToKilo => unitSize / power1,
            Differential.ByteToMega => unitSize / power2,
            Differential.ByteToGiga => unitSize / power3,
            Differential.ByteToTera => unitSize / power4,
            Differential.KiloToByte => unitSize * power1,
            Differential.KiloToMega => unitSize / power1,
            Differential.KiloToGiga => unitSize / power2,
            Differential.KiloToTera => unitSize / power3,
            Differential.MegaToByte => unitSize * power2,
            Differential.MegaToKilo => unitSize * power1,
            Differential.MegaToGiga => unitSize / power1,
            Differential.MegaToTera => unitSize / power2,
            Differential.GigaToByte => unitSize * power3,
            Differential.GigaToKilo => unitSize * power2,
            Differential.GigaToMega => unitSize * power1,
            Differential.GigaToTerra => unitSize / power1,
            Differential.TeraToByte => unitSize * power4,
            Differential.TeraToKilo => unitSize * power3,
            Differential.TeraToMega => unitSize * power2,
            Differential.TeraToGiga => unitSize * power1,
            _ => 0
        };
    }
}

public enum Differential
{
    ByteToKilo,
    ByteToMega,
    ByteToGiga,
    ByteToTera,
    KiloToByte,
    KiloToMega,
    KiloToGiga,
    KiloToTera,
    MegaToByte,
    MegaToKilo,
    MegaToGiga,
    MegaToTera,
    GigaToByte,
    GigaToKilo,
    GigaToMega,
    GigaToTerra,
    TeraToByte,
    TeraToKilo,
    TeraToMega,
    TeraToGiga
}

public enum StorageSizes : long
{
    Kilobyte = 1000,
    Megabyte = 1000000,
    Gigabyte = 1000000000,
    Terabyte = 1000000000000,
    Kibibyte = 1024,
    Mebibyte = 1048576,
    Gibibyte = 1073741824,
    Tebibyte = 1099511627776
}

public enum StorageBase
{
    Base2,
    Base10
}