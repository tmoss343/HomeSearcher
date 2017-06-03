var expect = require('chai').expect
var HouseConsumer = require('..')

describe('Houseconsumer', function() {
  it('should say hello', function(done) {
    expect(HouseConsumer()).to.equal('Hello, world');
    done();
  });
});
