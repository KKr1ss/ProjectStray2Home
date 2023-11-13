import { SafeUrl } from "@angular/platform-browser";
import { Observable } from "rxjs";
import { UserPreview } from "../../../user/common/models/user-preview";

export interface AnimalPreview {
  id: number;
  type: string;
  breed: string;
  name: string;
  sex: string;
  status: string;
  statusDate: Date;
  city: string;
  image$?: Observable<SafeUrl>
  userPreview: UserPreview
}
