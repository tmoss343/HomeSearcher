var fs = require('fs');
var google = require('googleapis');
var googleAuth = require('google-auth-library');
var json2csv = require('json2csv');
var Client = require('node-rest-client').Client;
var date = new Date();
var client = new Client();

google.auth.getApplicationDefault(function(err, authClient) {
    if (err) {
      return cb(err);
    }});
var SCOPES = ['https://www.googleapis.com/auth/drive.read.write'];

var drive = google.drive("v1");
// Load client secrets from a local file.
fs.readFile('client_secret.json', function processClientSecrets(err, content) {
  if (err) {
    console.log('Error loading client secret file: ' + err);
    return;
  }
  // Authorize a client with the loaded credentials, then call the
  // Drive API.
  authorize(JSON.parse(content), listFiles);
});


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

    var fileMetadata = {
      'name': 'HouseDataDump' + date.getDate + '/' + date.getMonth + '/' + date.getFullYear,
      'mimeType': 'application/vnd.google-apps.spreadsheet'
    };
    var media = {
      mimeType: 'text/csv',
      body: fs.createReadStream('data.csv')
    };
    drive.files.create({
      resource: fileMetadata,
      media: media,
      fields: 'id'
    }, function(err, file) {
      if(err) {
        // Handle error
        console.log(err);
      } else {
        console.log('File Id:' , file.id);
      }
    });
});
