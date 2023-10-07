using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VkNet.Abstractions;
using VkNet.Enums.StringEnums;
using VkNet.Model;
using VkNet.Utils;

namespace TimeLecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        /// <summary>
        /// Конфигурация приложения
        /// </summary>


        private readonly IConfiguration _configuration;

        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] GroupUpdate updates)
        {
            // Проверяем, что находится в поле "type" 
            switch (updates.Type.Value)
            {
                // Если это уведомление для подтверждения адреса
                case GroupUpdateType.Confirmation:
                    // Отправляем строку для подтверждения 
                    return Ok("7d529aed");


                // Новое сообщение
                case GroupUpdateType.MessageNew:
                    {
                        //var msg = new VkResponse(updates.Object);
                        _vkApi.Messages.Send(new MessagesSendParams
                        {
                            RandomId = new DateTime().Millisecond,
                            //PeerId = msg.PeerId.Value,
                            Message = "你好, 哎呀"
                        });
                        break;
                    }
            }
            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }
    }
}