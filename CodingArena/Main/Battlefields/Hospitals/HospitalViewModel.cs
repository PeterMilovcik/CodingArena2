namespace CodingArena.Main.Battlefields.Hospitals
{
    public class HospitalViewModel : Observable
    {
        public HospitalViewModel(Hospital hospital)
        {
            Hospital = hospital ?? throw new System.ArgumentNullException(nameof(hospital));
            X = hospital.Position.X;
            Y = hospital.Position.Y;
        }

        public Hospital Hospital { get; }
        public double X { get; }
        public double Y { get; }
    }
}