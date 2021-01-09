let uiUrl: string = location.origin;
uiUrl = uiUrl.endsWith("/") ? uiUrl.slice(0, -1) : uiUrl;
const dataServiceApi = uiUrl.replace(":4200", ":3000/api");
export const environment = {
  production: true,
  url: {
    dataServiceApi: dataServiceApi,
    uiUrl: uiUrl
  }
};
