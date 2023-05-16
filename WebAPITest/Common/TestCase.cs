namespace WebAPITest.Common
{
    public class TestCase<T1, T2>
    {
        public T1? Input { get; set; }
        public T2? ExpectedResult { get; set; }
    }
}
