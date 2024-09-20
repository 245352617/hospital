namespace YiJian.Handover
{
    public class DoctorPatientsConsts
    {  
        public static int MaxPatientIdLength { get; set; } = 20;
        public static int MaxPatientNameLength { get; set; } = 100;
        public static int MaxSexLength { get; set; } = 20;
        public static int MaxAgeLength { get; set; } = 10;
        public static int MaxTriageLevelLength { get; set; } = 100;
        public static int MaxDiagnoseLength { get; set; } = 1000;
        public static int MaxBedLength { get; set; } = 10;
        public static int MaxTestLength { get; set; } = 4000;
        public static int MaxInspectLength { get; set; } = 4000;
        public static int MaxEmrLength { get; set; } = 4000;
        public static int MaxInOutVolumeLength { get; set; } = 4000;
        public static int MaxVitalSignsLength { get; set; } = 4000;
        public static int MaxMedicineLength { get; set; } = 4000;
    }
}