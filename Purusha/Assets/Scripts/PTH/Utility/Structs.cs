namespace Structs
{
    public struct SEnemyData
    {
        public SEnemyData(EnemyData enemyData)
        {
            this.ID = enemyData.ID;
            this.Name = enemyData.Name;
            this.Level = enemyData.Level;
            this.Health = enemyData.Health;
            this.Atk = enemyData.Atk;
            this.Def = enemyData.Def;
            this.CriticalChance = enemyData.CriticalChance;
            this.CriticalDamage = enemyData.CriticalDamage;
            this.Avoid = enemyData.Avoid;
            this.Speed = enemyData.Speed;
            this.BreakGauge = enemyData.BreakGauge;
            this.PrefabPath = enemyData.PrefabPath;
            this.SpritePath = enemyData.SpritePath;
    }
        public int ID;
        public string Name;
        public int Level;
        public float Health;
        public float Atk;
        public float Def;
        public float CriticalChance;
        public float CriticalDamage;
        public float Avoid;
        public float Speed;
        public float BreakGauge;
        public string PrefabPath;
        public string SpritePath;
    }
}
