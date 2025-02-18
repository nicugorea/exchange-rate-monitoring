using V3._2;

var notificationService = new NotificationService();
var statisticsStore = new StatisticsStore();
var exchangeRateProvider = new ExchangeRateProvider();
var logger = new Logger();


while (true)
{
    try
    {
        var currentExchangeRate = await exchangeRateProvider.GetAsync(args.ElementAtOrDefault(0), logger);
        if (!currentExchangeRate.HasValue)
        {
            continue;
        }

        if (currentExchangeRate < statisticsStore.LowestExchangeRate)
        {
            notificationService.NotifyHighImportance();
        }
        else if (currentExchangeRate < statisticsStore.AverageExchangeRate)
        {
            notificationService.NotifyLowImportance();
        }

        logger.LogInformation($"Current exchange rate is: {currentExchangeRate}.");
        logger.LogInformation($"Lowest exchange rate is: {statisticsStore.LowestExchangeRate}.");
        logger.LogInformation($"Highest exchange rate is: {statisticsStore.HighestExchangeRate}.");
        logger.LogInformation($"Average exchange rate is: {statisticsStore.AverageExchangeRate}.");

        statisticsStore.Update(currentExchangeRate);
    }
    catch (Exception exception)
    {
        logger.LogError($"An error occured while deserializing the response content: {exception}");
        continue;
    }

    await Task.Delay(TimeSpan.FromMinutes(5));
}