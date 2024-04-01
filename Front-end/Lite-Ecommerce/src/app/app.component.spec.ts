import { AppComponent } from "./app.component";

describe('AppComponent', () => {
    let component: AppComponent;
    beforeEach(() => {
        component = new AppComponent();
    });


  test('should create the app', () => {
    expect(component).toBeTruthy();
  });

  test(`should have as title 'Lite-Ecommerce'`, () => {
    expect(component.title).toEqual('Lite-Ecommerce');
  });
});