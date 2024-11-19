# ImageEventsApi
install: dotnet tool install -g Amazon.Lambda.Tools

add new s3: aws s3 mb s3://image-events-deployment-123 --region us-east-1

deploy: dotnet lambda deploy-serverless --template serverless.template --stack-name image-events-api --s3-bucket image-events-deployment-123
