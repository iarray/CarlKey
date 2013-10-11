using Microsoft.Win32;

namespace CarlKey
{
    class RegeditLoader
    {
        /// <summary>
        /// 读取路径为keypath，键名为keyname的注册表键值，缺省返回def
        /// </summary>
        /// <param name="rootkey"></param>
        /// <param name="keypath">路径</param>
        /// <param name="keyname">键名</param>
        /// <param name="rtn">默认为null</param>
        /// <returns></returns>        
        static public bool GetRegVal(RegistryKey rootkey, string keypath, string keyname, out string rtn)
        {
            rtn = "";
            try
            {
                RegistryKey key = rootkey.OpenSubKey(keypath);
                rtn = key.GetValue(keyname).ToString();
                key.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
