using NRM.Models.DataModels;
using NRM.Models.ParcelModels;
using Sylvan.Data.Excel;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace NRM.Services
{
    public class ExcelService
    {
        private AppDbContext _context { get; set; }
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExcelService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Проверка на уникальность трек-номера посылки.
        /// </summary>
        /// <param name="trackNumber">Трек-номер посылки.</param>
        /// <returns>bool значение, true - трек-номер уникален.</returns>
        private bool ParcelRepeatCheck(string trackNumber)
        {
            int count = _context.Parcels.Where(w => !w.IsDeleted && w.TrackNumber == trackNumber).Count();
            if (count == 0) return true;
            else return false;
        }

        public async Task ImportExcel(IFormFile file, string login)
        {
            var edr = ExcelDataReader.Create(file.OpenReadStream(), ExcelWorkbookType.ExcelXml);

            List<Parcel> parcels = new List<Parcel>();

            var user = await _context.Users
                .Include(u => u.Place)
                .FirstAsync(w => w.Login == login);

            var DateNow = DateOnly.FromDateTime(DateTime.Now);
            var TimeNow = TimeOnly.FromDateTime(DateTime.Now);

            while (edr.Read())
            {
                string trackNumber = edr.GetString(0).Remove(edr.GetString(0).Length - 1);
                if (ParcelRepeatCheck(trackNumber))
                {
                    var type = _context.ParcelTypes.Where(w => !w.IsDeleted && w.Name == edr.GetString(5)).FirstOrDefault();
                    var typeId = type != null ? type.Id : 1;
                    if (typeId == 0) typeId = 1;

                    var parcel = new CreateModel()
                    {
                        TrackNumber = trackNumber,
                        Sender = edr.GetString(3),
                        Recipient = edr.GetString(4),
                        TypeId = typeId,
                        DepartureDate = DateNow,
                        DepartureTime = TimeNow,
                        StatusId = 1,
                        PlaceOfDepartureId = user.PlaceId
                    }.ToParcel();

                    parcel.LogParcels = new List<LogParcel>
                    {
                        new LogParcel
                        {
                            Parcel = parcel,
                            TypeId = 6,
                            Date = DateNow,
                            Time = TimeNow,
                            UserId = user.Id,
                            Message = $"Создана посылка с трек-номером {parcel.TrackNumber}. " +
                        $"Пользователь создавший посылку: {login}" +
                        $"({user.Place?.Name})" +
                        $". Время создания: {TimeOnly.FromDateTime(DateTime.Now)} {DateOnly.FromDateTime(DateTime.Now)}"
                        }
                    };

                    parcels.Add(parcel);
                }
            }

            if(parcels.Count > 0)
            {
                await _context.Parcels.AddRangeAsync(parcels);
                _context.SaveChanges();
            }
        }

        public async Task ExportExcel()
        {
            using var wbook = new XLWorkbook();

            var ws = wbook.Worksheets.Add("Лист1");

            var parcels = _context.Parcels.Where(w => !w.IsDeleted).Include(i => i.Status).Include(i => i.Type).ToList();

            ws.Cell("A1").Value = "150";

            ws.Name = "Список посылок";

            string[] header = new string[]
            {
                "Трек-номер",
                "Статус",
                "Отправитель",
                "Получатель",
                "Тип посылки",
                "Дата отправки"
            };

            for (int i = 0; i < header.Length; i++)
            {
                ws.Cell(1, i + 1).Value = header[i];
            }

            for (int i = 1; i <= parcels.Count; i++)
            {
                ws.Cell(i + 1, 1).Value = String.Format(parcels[i - 1].TrackNumber, i, 1);
                ws.Cell(i + 1, 2).Value = String.Format(parcels[i - 1].Status.Name, i, 1);
                ws.Cell(i + 1, 3).Value = String.Format(parcels[i - 1].Sender, i, 2);
                ws.Cell(i + 1, 4).Value = String.Format(parcels[i - 1].Recipient, i, 3);
                ws.Cell(i + 1, 5).Value = String.Format(parcels[i - 1].Type.Name, i, 4);
                ws.Cell(i + 1, 6).Value = String.Format(parcels[i - 1].DepartureDate.ToShortDateString(), i, 5);
            }

            ws.Columns().AdjustToContents();

            wbook.SaveAs(_webHostEnvironment.WebRootPath + @"\excel\parcels.xlsx");
        }
    }
}
