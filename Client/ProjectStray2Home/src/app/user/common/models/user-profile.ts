import { SafeUrl } from "@angular/platform-browser";
import { Observable } from "rxjs";
import { AnimalPreview } from "../../../animal/common/models/animal-preview";

export interface UserProfile
{
  id: string;
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  sex: string;
  dateOfBirth: Date;
  phoneNumber: string;
  currentCity: string;
  description?: string;
  createDate?: Date;
  updateDate?: Date;
  image$?: Observable<SafeUrl>

  animals: AnimalPreview[];
  user_Cities: string[];
}
