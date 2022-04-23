namespace DungeonPapperWPF
{
    public class FieldDto
    {
        public int x { set; get; }
        public int y { set; get; }
        public Barrier? leftBarrier { set; get; }
        public Barrier? rightBarrier { set; get; }
        public Barrier? topBarrier { set; get; }
        public Barrier? downBarrier { set; get; }

        public bool trap { set; get; }
        public Monster monster { set; get; }
        public Prey prey { set; get; }

        public Boss one { set; get; }
        public Boss two { set; get; }
        public Boss three { set; get; }
    }
}