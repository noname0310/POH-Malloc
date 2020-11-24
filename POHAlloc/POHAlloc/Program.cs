using System;

unsafe
{
    var foo = Malloc<int>(sizeof(int)).value;
    *foo = 10;
    Console.WriteLine(foo->ToString());

    //safe
    var bar = Malloc<int>(sizeof(int));
    *bar.value = 10;
    Console.WriteLine(bar.value->ToString());
}

unsafe static UnManaged<T> Malloc<T>(int size) where T : unmanaged
{
    byte[] pinnedArr = GC.AllocateUninitializedArray<byte>(size, true);
    fixed (byte* ptr = pinnedArr) { return new((T*)ptr, pinnedArr); }
}

unsafe struct UnManaged<T> where T : unmanaged
{
    public T* value;
    public byte[] src;

    public UnManaged(T* value, byte[] src)
    {
        this.value = value;
        this.src = src;
    }
}
