import { SafeUrl } from "@angular/platform-browser";
import { Observable } from "rxjs";

export interface AnimalImage {
  id: number,
  image$?: Observable<SafeUrl>
}
