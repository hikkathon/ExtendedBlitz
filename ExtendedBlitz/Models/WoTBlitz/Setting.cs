namespace ExtendedBlitz.Models.WoTBlitz
{
    internal class Setting
    {
        /// <summary> Идентификатор приложения </summary>
        public string application_id { get; set; }
        /// <summary> Идентификатор аккаунта игрока. Максимальное ограничение: 100. </summary>
        public string account_id { get; set; }
        /// <summary> Ключ доступа к личным данным аккаунта пользователя; можно получить при помощи метода авторизации; действителен в течение определённого времени </summary>
        public string access_token { get; set; }
        /// <summary> Список дополнительных полей, которые будут включены в ответ. Допустимые значения</summary>
        public string extra { get; set; }
        /// <summary> Язык локализации. По умолчанию: "ru". Допустимые значения: </summary>
        public string language { get; set; }
    }
}