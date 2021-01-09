import {Injectable} from '@angular/core';
import DataSource from "devextreme/data/data_source";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {TodoListEnum} from "../enum/todo-list.enum";
import {AlertService} from "../../utils/alert.service";
import {SweetAlertIconEnum} from "../../utils/sweet-alert-icon.enum";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class TodoListService {
  protected _dataSource: DataSource;
  protected _baseUrl: string;

  constructor(
    private http: HttpClient,
    private alertService: AlertService
  ) {
    this._baseUrl = environment.url.dataServiceApi + TodoListEnum.CRUD_ENDPOINT
    this._dataSource = new DataSource({
      key: "id",
      load: loadOptions => {
        return this.http
          .post(
              this._baseUrl + "/Load",
            loadOptions,
          )
          .toPromise()
          .then(result => {
            return result;
          })
          .catch((err: HttpErrorResponse) => {
            this.showErrorToaster(err);
            return Promise.reject("promise rejected");
          });
      },
      insert: values => {
        return this.http
          .post(
            this._baseUrl,
            values,
          )
          .toPromise()
          .catch((err: HttpErrorResponse) => {
            this.showErrorToaster(err);
            return Promise.reject("promise rejected");
          });
      },
      update: (id, values) => {
        return this.http
          .put(
            this._baseUrl + "/" + encodeURIComponent(id),
            values,
          )
          .toPromise()
          .catch((err: HttpErrorResponse) => {
            this.showErrorToaster(err);
            return Promise.reject("promise rejected");
          });
      },
      remove: id => {
        return this.http
          .delete(
            this._baseUrl + "/" + encodeURIComponent(id),
          )
          .toPromise()
          .catch((err: HttpErrorResponse) => {
            this.showErrorToaster(err);
            return Promise.reject("promise rejected");
          });
      }
    });
  }

  getDataSource(): DataSource {
    return this._dataSource;
  }

  load() {
    return this._dataSource.store().load();
  }

  insert(obj) {
    return this._dataSource.store().insert(obj);
  }

  update(id: number, obj) {
    return this._dataSource.store().update(id, obj);
  }

  remove(id: number) {
    return this._dataSource.store().remove(id);
  }

  public showErrorToaster(err: HttpErrorResponse) {
    let errors: object = {};
    if(err.error.errors) {
      errors = err.error.errors;
      for (const [title, errorMessages] of Object.entries(errors)) {
        this.alertService.show(
          title,
          errorMessages.join(", ") + " <br>",
          SweetAlertIconEnum.Error
        );
      }
    }
    else {
        this.alertService.show("Error Code: " + err.error.Code, err.error.Message, SweetAlertIconEnum.Error);
    }
  }
}
