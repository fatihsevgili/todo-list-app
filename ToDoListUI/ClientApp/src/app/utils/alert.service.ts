import {Injectable} from "@angular/core";
import Swal, {
  SweetAlertOptions,
  SweetAlertIcon
} from "sweetalert2";

@Injectable({
  providedIn: "root"
})
export class AlertService {
  constructor() {}
   show(
    title: string,
    html: string,
    icon: SweetAlertIcon,
    showConfirmButton: boolean = true
  ) {
    let config: SweetAlertOptions;
      config = {
        title: title,
        html: html,
        icon: icon,
        showConfirmButton: showConfirmButton,
        timer: showConfirmButton ? undefined : 2500,
        showCloseButton: true
    };
    if (showConfirmButton) {
      config.allowOutsideClick = false;
    }
    return Swal.fire(config);
  }
}
