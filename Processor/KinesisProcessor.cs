using Amazon.Lambda.Core;
using ImageEventsApi.Models;
using System.Text.Json;
using System.Text;
using ImageEventsApi.Repositories;
using Amazon.Lambda.KinesisEvents;

namespace ImageEventsApi.Processor
{
    public class KinesisProcessor
    {
        private readonly IImageEventService _imageEventService;
        private readonly ILogger<KinesisProcessor> _logger;

        public KinesisProcessor()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(logging => logging.AddConsole())
                .AddSingleton<IImageEventService, ImageEventService>()
                .BuildServiceProvider();

            _imageEventService = serviceProvider.GetRequiredService<IImageEventService>();
            _logger = serviceProvider.GetRequiredService<ILogger<KinesisProcessor>>();
        }

        [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public async Task Handler(KinesisEvent kinesisEvent, ILambdaContext context)
        {
            foreach (var record in kinesisEvent.Records)
            {
                try
                {
                    var json = Encoding.UTF8.GetString(record.Kinesis.Data.ToArray());
                    var imageEvent = JsonSerializer.Deserialize<ImageEvent>(json);
                    _imageEventService.AddEvent(imageEvent);
                    _logger.LogInformation("Processed Kinesis record: {Json}", json);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing Kinesis record");
                }
            }
        }
    }
}
