// async route
app.get('/test', async (req, res) => {
    const msg = await timeout(()=>{console.log('2000 years passed')});
    res.send(msg);
  });

it('', async () => {
const res = await request(app)
    .get('/test');
expect(res.text).toEqual('1s passed');
expect(res.status).toEqual(200);
});

// async function
const timeout = (callback) => new Promise((resolve, reject) => {
    setTimeout(() => {
      console.log('1000 years passed');
      callback();
      resolve('1s passed');
    }, 3000);
});
  
it('', async () => {
const res = await timeout(() => {
    console.log('2000 years passed')
});
expect(res).toEqual('1s passed');
});

// Failed to close the port after executing all the tests
beforeAll((done) => {
  server = app.listen(4000, (err) => {
    if (err) return done(err);

    agent = request.agent(server); // since the application is already listening, it should use the allocated port
    done();
  });
});

afterAll(async (done) => {
  console.log('The server is closing');
  await new Promise(resolve => setTimeout(() => resolve(), 500)); // avoid jest open handle error
  return  server && server.close(done);
});