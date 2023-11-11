/* eslint-disable prefer-const */
import { formatDate } from "@angular/common";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { filter, Observable} from "rxjs";
import { environment } from "../../../environments/environment";
import { APIResult } from "../../common/api-result";
import { PaginatorResult } from "../../common/paginator-result";
import { AnimalCommentRequest } from "./animal-comment-request";
import { AnimalDetails } from "./animal-details";
import { AnimalFilter } from "./animal-filter";
import { AnimalPreview } from "./animal-preview";
import { AnimalRequest } from "./animal-request";

@Injectable()
export class AnimalService {
  constructor(protected http: HttpClient) { }

  upload(item: AnimalRequest, files: File[]): Observable<APIResult> {
    let url = environment.baseUrl + "api/Animal/Upload";

    let formData: FormData = new FormData();
    for (let i = 0; i < files.length; i++) {
      formData.append("animalImages", files[i], (i + 1) + ".image")
    }
    Object.entries(item).forEach(([key, value]) => {
      if (key != "dateOfBirth") formData.append(key, value);
    });
    if (item.dateOfBirth)
      formData.append("dateOfBirth", formatDate(item.dateOfBirth, "yyyy-MM-dd", "en"));

    return this.http.post<APIResult>(url, formData);
  }

  uploadComment(comment: AnimalCommentRequest): Observable<APIResult> {
    let url = environment.baseUrl + "api/Animal/UploadComment";
    return this.http.post<APIResult>(url, comment);
  }

  getAnimals(pageIndex: number, pageSize: number, filters?: AnimalFilter): Observable<PaginatorResult<AnimalPreview>> {
    let url = environment.baseUrl + "api/Animal/GetAnimals";
    let params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("name", (filters?.name === undefined ? "" : filters!.name))
      .set("status", (filters?.status === undefined ? "" : filters!.status))
      .set("city", (filters?.city === undefined ? "" : filters!.city))
      .set("type", (filters?.type === undefined ? "" : filters!.type))
      .set("sex", (filters?.sex === undefined ? "" : filters!.sex))

    return this.http.get<PaginatorResult<AnimalPreview>>(url, { params });
  }

  getAnimalDetails(id: number): Observable<AnimalDetails> {
    let url = environment.baseUrl + "api/Animal/GetAnimalDetails/";
    return this.http.get<AnimalDetails>(url + id)
  }
}
