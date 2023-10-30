import { SafeUrl } from "@angular/platform-browser";
import { Observable } from "rxjs";

export interface UserPreview {
  id: string;
  userName: string;
  sex: string;
  email: string;
  phoneNumber: string;
  firstName: string;
  lastName: string;
  currentCity: string;
  image$?: Observable<SafeUrl>;
}
