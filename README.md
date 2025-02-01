<body>

<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

  
  <h1>API Gateway - EA Platform</h1>
    <p>This repository contains the API Gateway of the <strong>EA Platform</strong>, an Ecommerce microservice. The Gateway is responsible for routing requests to internal services and ensuring the consistency of received and sent data.</p>

   <h2>Architecture</h2>
    
  ![EA](https://github.com/user-attachments/assets/acc5c9b7-2d0d-45cf-a839-a91be2dbc79f)

   <h2>Microservices Repositories</h2>
    <ul>
        <li><strong>Customer API:</strong> <a href="https://github.com/Guidev123/EA-Customer">https://github.com/Guidev123/EA-Customer</a></li>
        <li><strong>Cart API:</strong> <a href="https://github.com/Guidev123/EA-Cart">https://github.com/Guidev123/EA-Cart</a></li>
        <li><strong>Catalog API:</strong> <a href="https://github.com/Guidev123/EA-Catalog">https://github.com/Guidev123/EA-Catalog</a></li>
        <li><strong>Order API:</strong> <a href="https://github.com/Guidev123/EA-Catalog">https://github.com/Guidev123/EA-Catalog</a></li>
        <li><strong>Payment API:</strong> <a href="https://github.com/Guidev123/EA-Payments">https://github.com/Guidev123/EA-Payments</a></li>
        <li><strong>Identity API:</strong> <a href="https://github.com/Guidev123/EA-Identity">https://github.com/Guidev123/EA-Identity</a></li>
    </ul>

   <h2>Authentication</h2>
    <p>The frontend will consume this Gateway directly, in addition to the <strong>Identity</strong> API, which is responsible for generating asymmetric key JWT tokens for authentication across all APIs.</p>

   <h2>Communication</h2>
    <ul>
        <li>The Gateway communicates with the microservices via <strong>REST</strong>.</li>
        <li>Microservices communicate with each other using <strong>RabbitMQ</strong> and <strong>REST</strong>.</li>
    </ul>

   <h2>Validations</h2>
    <p>The API Gateway implements business validations to ensure the integrity and consistency of data transmitted between the frontend and the microservices.</p>
</body>
