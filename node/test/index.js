var expect = require('chai').expect
var ZillowConsumer = require('..')

describe('zillowconsumer', function() {
  it('should say hello', function(done) {
    expect(ZillowConsumer()).to.equal('Hello, world');
    done();
  });
});
