using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.BussinessLayer.Manager
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }
    public static class Constant
    {
        public static string SuccessMessage
        {
            get
            {
                return "İşleminiz başarılı bir şekilde gerçekleşmiştir.";
            }
        }

        public static string ErrorMessage
        {
            get
            {
                return "İşleminiz gerçekleştirilirken bir hata oluştu.";
            }
        }
    }
    public class ManagerClasses
    {
       public static string[] aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };
        public static string MonthTurkce(int month)
        {
                return aylar[month-1];
        }

        public static int MonthTurkceInt(string month)
        {
            for (int i = 0; i < aylar.Length; i++)
            {
                if (aylar[i] == month)
                {
                    return i+1;
                }
            }
            return 0;
        }

    }

   

}
