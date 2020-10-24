namespace Code.Scripts
{
    public class Common 
    {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP_8_1
        public static readonly bool IsMobile = true;
#else
        public static readonly bool IsMobile = false;
#endif
        public static void Swap<T> (ref T lhs, ref T rhs) {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }
    }
    
}