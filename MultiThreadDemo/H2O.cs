// 1117. H2O 生成
public class H2O
{
    private SemaphoreSlim ot = new SemaphoreSlim(0, 1);
    private SemaphoreSlim ht = new SemaphoreSlim(2, 2);
    public H2O()
    {

    }

    public void Hydrogen(Action releaseHydrogen)
    {
        ht.Wait();
        // releaseHydrogen() outputs "H". Do not change or remove this line.
        releaseHydrogen();
        if (ht.CurrentCount == 0)
            ot.Release();
    }

    public void Oxygen(Action releaseOxygen)
    {
        ot.Wait();
        // releaseOxygen() outputs "O". Do not change or remove this line.
        releaseOxygen();
        ht.Release(2);
    }
}