# ServiceFabricHmacDemo
This solution demonstrates how to create a stateful service application using Microsoft's Azure Service Fabric. You will need to use Visual Studio and install the Service Fabric SDK to run this Sample. 

### Glossary
**HMAC** - Hashed Message Authentication Code
  https://en.wikipedia.org/wiki/Hash-based_message_authentication_code

### Projects are as follows:
* __WebApi__

    Accepts Http POST requests from clients to the "transactions" controller. The request is expected to have an "X-HMAC" header. The api then uses the ReliableHmacRepository to query the HmacService for previously existing requests with the same HMAC.

* __HmacService__ 

    Stateful service that maintains a collection of previously seen HMACs. It can be queried for existance of an HMAC. New HMACs are added to the collection which is made reliable by the Service Fabric. 

* __HmacDomain__ 

    Contains the service contract that is exposed by the HmacService. Referenced by both the WebApi & HmacService. 
* __HmacDemo__ 

    Deployment project for Azure Service Fabric. 
