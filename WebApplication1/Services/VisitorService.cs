using AutoMapper;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using WebApplication1.Common;
using WebApplication1.CosmosDB;
using WebApplication1.DTO;
using WebApplication1.Interfaces;
using Visitor = WebApplication1.Entities.Visitor;

namespace WebApplication1.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly ICosmosDbService _cosmosDBService;
        private readonly IMapper _mapper;

        public VisitorService(ICosmosDbService cosmosDbService, IMapper mapper)
        {
            _cosmosDBService = cosmosDbService;
            _mapper = mapper;
        }

        public async Task<VisitorDTO> AddVisitor(VisitorDTO visitoModel)
        {
            
            var visitor = _mapper.Map<Visitor>(visitoModel);

            visitor.Intialize(true, Credentials.VisitorDocumenttype = "visitor", "Shubham", "Shubham");

            var response = await _cosmosDBService.AddVisitor(visitor);

            var responseModel = _mapper.Map<VisitorDTO>(response);

            // Sending mail to the 
            string subject = "Visitor Approval Request By Security Clearance System";
            string toEmail = "tidkeshubham10@gmail.com";
            string username = visitoModel.Name;
            string message = $@"
Hello Team,

We have a new visitor registered at CentraLogic premises.

🔹 **Name**: {visitoModel.Name}
🔹 **Email**: {visitoModel.Email}
🔹 **Phone**: {visitor.PhoneNumber}
🔹 **Company**: {visitor.CompanyName}
🔹 **Purpose of Visit**: {visitor.Purpose}
🔹 **Entry Time**: {visitor.EntryTime:MMMM dd, yyyy - HH:mm tt}
🔹 **Expected Exit Time**: {visitor.ExitTime:MMMM dd, yyyy - HH:mm tt}

Please ensure to extend a warm welcome and assist them as needed.

Best regards,
[Tidke shubham]
";

            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(subject, toEmail, username, message).Wait();

            return responseModel;
        }
        public async Task<IEnumerable<VisitorDTO>> GetAllVisitors()
        {
            var visitors = await _cosmosDBService.GetAll<Visitor>();
            return visitors.Select(MapEntityToDTO).ToList();
        }
        public async Task DeleteVisitor(string id)
        {
            await _cosmosDBService.DeleteVisitor(id);
        }

        public async Task<VisitorDTO> GetVisitorById(string id)
        {
            var visitor = await _cosmosDBService.GetVisitorById(id); 
            return _mapper.Map<VisitorDTO>(visitor);
        }

        public async Task<VisitorDTO> UpdateVisitor(string id, VisitorDTO visitorModel)
        {
            var visitorEntity = await _cosmosDBService.GetVisitorById(id);
            if (visitorEntity == null)
            {
                throw new Exception("Manager not found");
            }
            visitorEntity = _mapper.Map<Visitor>(visitorModel); ;
            visitorEntity.Id = id;
            var response = await _cosmosDBService.Update(visitorEntity);
            return _mapper.Map<VisitorDTO>(response);
        }

      

        public async Task<VisitorDTO> UpdateVisitorStatus(string visitorId, bool newStatus)
        {
            if (string.IsNullOrEmpty(visitorId))
                throw new ArgumentException("Visitor ID cannot be null or empty", nameof(visitorId));

            var visitor = await _cosmosDBService.GetVisitorById(visitorId);
            if (visitor == null)
                throw new Exception("Visitor not found");

            visitor.PassStatus = newStatus;
            await _cosmosDBService.Update(visitor);

            string subject = "Your Visitor Status Has Been Updated";
            string toEmail = visitor.Email;
            string userName = visitor.Name;

            if (string.IsNullOrEmpty(toEmail))
                throw new Exception("Visitor email is null or empty");

            if (string.IsNullOrEmpty(userName))
                throw new Exception("Visitor name is null or empty");

            string message = $"Hello {visitor.Name},\n\n" +
                $"Thank you for submitting your visit pass request to CentraLogic. Your Visitor ID is {visitor.Id}. Kindly keep this ID for future reference.\n\n" +
                "We acknowledge receipt of your request and want to assure you that our team is in the process of reviewing and taking necessary actions. You can use the provided Visitor ID to check the status of your request.\n\n" +
                "If you have any immediate concerns or require further information, please feel free to contact our dedicated manager at nisarga.v.jamdhare@gmail.com. We appreciate your patience and look forward to welcoming you soon.\n\n" +
                "Best regards,\n" +
                "Tidke Shubham\n" +
                "Developer\n" +
                "Centralogic";

            byte[] pdfBytes = null;
            if (newStatus)
            {
                pdfBytes = GenerateVisitorPassPdf(visitor);
            }

            EmailSender emailSender = new EmailSender();
            await emailSender.SendEmail(subject, toEmail, userName, message, pdfBytes);

            return new VisitorDTO
            {
                Id = visitor.Id,
                Name = visitor.Name,
                Email = visitor.Email,
                PassStatus = visitor.PassStatus,
            };
        }

        private Visitor MapDTOToEntity(VisitorDTO visitorModel)
        {
            return new Visitor
            {
                Id = visitorModel.Id,
                Name = visitorModel.Name,
                Email = visitorModel.Email,
                PhoneNumber = visitorModel.PhoneNumber,
                Address = visitorModel.Address,
                CompanyName = visitorModel.CompanyName,
                Purpose = visitorModel.Purpose,
                EntryTime = visitorModel.EntryTime,
                ExitTime = visitorModel.ExitTime,
                Role = "visitor",
                PassStatus = false,
            };
        }

        private VisitorDTO MapEntityToDTO(Visitor visitorEntity)
        {
            if (visitorEntity == null) return null;
            return new VisitorDTO
            {
                Id = visitorEntity.Id,
                Name = visitorEntity.Name,
                Email = visitorEntity.Email,
                PhoneNumber = visitorEntity.PhoneNumber,
                Address = visitorEntity.Address,
                CompanyName = visitorEntity.CompanyName,
                Purpose = visitorEntity.Purpose,
                EntryTime = visitorEntity.EntryTime,
                ExitTime = visitorEntity.ExitTime,
                Role = "visitor",
                PassStatus = false
            };
        }



        private byte[] GenerateVisitorPassPdf(Visitor visitor)
        {
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));

            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Define fonts
                XFont titleFont = new XFont("Arial", 20);
                XFont normalFont = new XFont("Arial", 12);

                // Draw title
                gfx.DrawString("Visitor Pass", titleFont, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormats.Center);

                // Draw visitor details
                int yOffset = 60;
                gfx.DrawString($"Name: {visitor.Name}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width - 100, page.Height), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Email: {visitor.Email}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width - 100, page.Height), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Phone: {visitor.PhoneNumber}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width - 100, page.Height), XStringFormats.TopLeft);
                yOffset += 20;
                gfx.DrawString($"Purpose of Visit: {visitor.Purpose}", normalFont, XBrushes.Black, new XRect(50, yOffset, page.Width - 100, page.Height), XStringFormats.TopLeft);

                // Save the PDF to memory stream
                document.Save(ms);
                ms.Position = 0;

                return ms.ToArray();
            }
        }

        public Task<List<VisitorDTO>> GetVisitorsByStatus(bool status)
        {
            throw new NotImplementedException();
        }
    }
}
