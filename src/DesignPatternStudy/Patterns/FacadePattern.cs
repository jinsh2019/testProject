namespace DesignPatternStudy.Patterns
{
    class SubSystem1
    {
        public bool isTrue()
        {
            return true;
        }
    }

    class SubSystem2
    {
        public bool isOk()
        {
            return true;
        }
    }
    class SubSystem3
    {
        public bool isGoodMan()
        {
            return false;
        }
    }

    class Facade
    {
        SubSystem1 s1 = new SubSystem1();
        SubSystem2 s2 = new SubSystem2();
        SubSystem3 s3 = new SubSystem3();

        public bool prove()
        {
            return s1.isTrue() && s2.isOk() && s3.isGoodMan();
        }
    }
}