import {Component} from "@angular/core";
import DataSource from "devextreme/data/data_source";
import {TodoListService} from "../service/todo-list.service";
import {TodoListModel} from "../model/todo-list.model";
import {AlertService} from "../../utils/alert.service";
import {SweetAlertIconEnum} from "../../utils/sweet-alert-icon.enum";

@Component({
  selector: 'todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent {

  dataSource: DataSource;
  selectedRowKey: number;
  textBoxValue: string;
  columns: Array<any>;

  constructor(
    private service: TodoListService,
    private alertService: AlertService
  ) {
    this.dataSource = <any>this.service.getDataSource();
  }

  ngOnInit() {
    this.columns = [
      {
        dataField: "completed",
        dataType: "boolean",
        caption: "Status",
        cellTemplate: "checkBoxTemplate",
        allowFiltering: false,
        width: 80
      },
      {
        dataField: "description",
        dataType: "string",
        caption: "Task Description",
        allowFiltering: true
      },
      {
        caption: "Action",
        cellTemplate: "actionMenuTemplate",
        allowEditing: false,
        allowFiltering: false,
        alignment: "center",
        width: 80
      }
    ];
  }

  ngOnDestroy() {
  }

  onCellPrepared(e) {
    if (e.rowType == "header") {
      e.cellElement.style.cssText = "font-weight: bold; font-size: 16px;;";
    }
  }

  onSubmit(event: any): void {
    if (this.textBoxValue) {
      let requestItem = new TodoListModel()
      requestItem.description = this.textBoxValue;
      requestItem.completed = false;
      this.service.insert(requestItem).then(() => {
        this.dataSource.reload();
        this.textBoxValue = ""
      })
    } else {
      this.alertService.show("Warning", "Please type a task description", SweetAlertIconEnum.Warning)
    }
  }

  onRowClick(event: any): void {
    if (event.rowType == "data") {
      this.selectedRowKey = event.key;
    }
  }

  onCellClick(event: any): void {
    if (event.rowType == "data" && event.column.dataField == "description") {
      let rowData: TodoListModel = event.data;
      rowData.completed = !rowData.completed;
      this.service.update(rowData.id, rowData).then(() => {
        this.dataSource.reload();
      })
    }
  }

  onDeleteButtonClick(event: any): void {
    setTimeout(() => {
      this.service.remove(this.selectedRowKey).then(() => {
        this.dataSource.reload();
      });
    }, 0)
  }
}
