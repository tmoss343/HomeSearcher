import { HouseSearchPage } from './app.po';

describe('house-search App', () => {
  let page: HouseSearchPage;

  beforeEach(() => {
    page = new HouseSearchPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
