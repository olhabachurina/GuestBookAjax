using System.ComponentModel.DataAnnotations;

namespace GuestBookAjax.Models
{
   
        public class MessageViewModel
        {
            [Required(ErrorMessage = "Отзыв обязателен.")]
            public string Content { get; set; }

            [Required(ErrorMessage = "Email обязателен.")]
            [EmailAddress(ErrorMessage = "Некорректный Email.")]
            public string Email { get; set; }

            [Url(ErrorMessage = "Некорректный URL.")]
            public string WWW { get; set; }

            public MessageViewModel()
            {
                Content = string.Empty;
                Email = string.Empty;
                WWW = string.Empty;
            }
        }
    }


