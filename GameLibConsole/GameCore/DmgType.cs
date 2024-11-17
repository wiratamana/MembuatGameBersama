using System;

namespace GameCore
{
    public struct DMGType
    {
        public int PhysicalDMGBoost;
        public int FireDMGBoost;
        public int IceDMGBoost;
        public int LightningDMGBoost;
        public int WindDMGBoost;
        public int QuantumDMGBoost;
        public int ImaginaryDMGBoost;

        public int PhysicalRESBoost;
        public int FireRESBoost;
        public int IceRESBoost;
        public int LightningRESBoost;
        public int WindRESBoost;
        public int QuantumRESBoost;
        public int ImaginaryRESBoost;

        public int PhysicalPENBoost;
        public int FirePENBoost;
        public int IcePENBoost;
        public int LightningPENBoost;
        public int WindPENBoost;
        public int QuantumPENBoost;
        public int ImaginaryPENBoost;

        public float PhysicalDMGBoostMultiplier => PhysicalDMGBoost / 100.0f;
        public float FireDMGBoostMultiplier => FireDMGBoost / 100.0f;
        public float IceDMGBoostMultiplier => IceDMGBoost / 100.0f;
        public float LightningDMGBoostMultiplier => LightningDMGBoost / 100.0f;
        public float WindDMGBoostMultiplier => WindDMGBoost / 100.0f;
        public float QuantumDMGBoostMultiplier => QuantumDMGBoost / 100.0f;
        public float ImaginaryDMGBoostMultiplier => ImaginaryDMGBoost / 100.0f;

        public float PhysicalRESBoostMultiplier => PhysicalRESBoost / 100.0f;
        public float FireRESBoostMultiplier => FireRESBoost / 100.0f;
        public float IceRESBoostMultiplier => IceRESBoost / 100.0f;
        public float LightningRESBoostMultiplier => LightningRESBoost / 100.0f;
        public float WindRESBoostMultiplier => WindRESBoost / 100.0f;
        public float QuantumRESBoostMultiplier => QuantumRESBoost / 100.0f;
        public float ImaginaryRESBoostMultiplier => ImaginaryRESBoost / 100.0f;

        public float PhysicalPENBoostMultiplier => PhysicalPENBoost / 100.0f;
        public float FirePENBoostMultiplier => FirePENBoost / 100.0f;
        public float IcePENBoostMultiplier => IcePENBoost / 100.0f;
        public float LightningPENBoostMultiplier => LightningPENBoost / 100.0f;
        public float WindPENBoostMultiplier => WindPENBoost / 100.0f;
        public float QuantumPENBoostMultiplier => QuantumPENBoost / 100.0f;
        public float ImaginaryPENBoostMultiplier => ImaginaryPENBoost / 100.0f;
    }
}
