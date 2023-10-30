import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { DomSanitizer, SafeUrl } from "@angular/platform-browser";
import { Observable } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: 'root',
})
export class ImageDownloadService {
  url = environment.baseUrl + "api/Image/";

  constructor(
    private http: HttpClient,
    private sanitizer: DomSanitizer
  ) { }

  getUserProfileImage(username: string): Observable<Blob> {
    let customUrl = this.url + "GetUserImage/" + username;
    return this.http.get(customUrl
      , {
        responseType: 'blob'
      }
    );
  }

  getAnimalImage(animalId: number): Observable<Blob> {

    let customUrl = this.url + "GetAnimalImage/" + animalId;
    return this.http.get(customUrl
      , {
        responseType: 'blob'
      }
    );
  }

  getAnimalImageByImageId(animalId: number, imageId: number): Observable<Blob> {
    let customUrl = this.url + "GetAnimalImage/" + animalId;
    let param = new HttpParams()
    .set("imageId", imageId.toString());
    return this.http.get(customUrl
      , {
        params: param,
        responseType: 'blob'
      }
    );
  }

  convertBlobToSafeUrl(blob: Blob): SafeUrl {
    const objectURL = URL.createObjectURL(blob);
    const img = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    return img;
  }
}
