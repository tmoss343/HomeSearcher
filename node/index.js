var fs = require('fs');
var json2csv = require('json2csv');
var Client = require('node-rest-client').Client;
var client = new Client();

var fields = ['id', 'lastsoldprice', 'lastsolddate', 'finishedSqFt', 'bathrooms', 'bedrooms', 'zestimate', 'lastUpdated', 'address', 'url'];
// direct way
client.get("http://localhost:5000/api/houses/propertysearchresults", function (json, response) {
    // parsed response body as js object
    // console.log(data);
    // raw response
    // console.log(response);
    var result = json2csv({ data: json, fields: fields });
    fs.writeFile("data.csv", result/*JSON.stringify(data)*/, function(err) {
      if(err) {
          return console.log(err);
      }

      console.log("The data was saved!");
    });
});
