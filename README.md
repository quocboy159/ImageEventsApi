# ImageEventsApi
install: dotnet tool install -g Amazon.Lambda.Tools

1. add new s3: aws s3 mb s3://image-events-deployment-123 --region ap-southeast-2

2. release code to get local path zip: dotnet lambda package --configuration Release --framework net8.0 --output-package code.zip

3. publish code zip to s3: aws s3 cp D:\Learning\ImageEventsApi\code.zip s3://image-events-deployment-123/code.zip

4. deploy: dotnet lambda deploy-serverless --template serverless.template --stack-name image-events-api --s3-bucket image-events-deployment-123 --parameter-overrides "Environment=Production"

5. destroy resource: 
  - aws cloudformation delete-stack --stack-name image-events-api
  - aws s3 rm s3://image-events-deployment-123 --recursive
  - aws s3 rb s3://image-events-deployment-123
  - dotnet lambda delete-serverless --stack-name image-events-api

