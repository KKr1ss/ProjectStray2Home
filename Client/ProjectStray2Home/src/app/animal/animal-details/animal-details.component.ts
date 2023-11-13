import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { catchError, delay, map, Observable, of, retryWhen, tap } from 'rxjs';
import { ImageDownloadService } from '../../common/image-download.service';
import { APIResult } from '../../common/models/api-result';
import { AssetUrls } from '../../shared/asset-urls';
import { AuthService } from '../../user/common/auth.service';
import { AnimalService } from '../common/animal.service';
import { AnimalCommentRequest } from '../common/models/animal-comment-request';
import { AnimalDetails } from '../common/models/animal-details';
import { AnimalImage } from '../common/models/animal-image';

@Component({
  selector: 'app-animal-details',
  templateUrl: './animal-details.component.html',
  styleUrls: ['./animal-details.component.scss']
})
export class AnimalDetailsComponent implements OnInit {
  animal$?: Observable<AnimalDetails>;
  id!: number;
  animalImages: AnimalImage[] = [];
  selectedImage?: SafeUrl;
  isBusy: boolean = false;
  form!: FormGroup;
  commentResult?: APIResult;
  isLoggedIn: boolean = false;
  chipDescription: string = "Ismeretlen";

  constructor(
    private authService: AuthService,
    private animalService: AnimalService,
    private activatedRoute: ActivatedRoute,
    private imageDownloadService: ImageDownloadService
  ) { }

  ngOnInit(): void {
    this.isBusy = true;
    this.form = new FormGroup({
      comment: new FormControl('', Validators.required)
    });

    if (this.authService.isAuthenticated()) {
      this.isLoggedIn = true;
    }

    const idParam = this.activatedRoute.snapshot.paramMap.get('id');

    this.id = idParam ? +idParam : 0;
    this.getData();
  }

  getData(): void {
    this.animal$ = this.animalService.getAnimalDetails(this.id).pipe(tap((x: AnimalDetails) => {
      if (x.isChipped == true) this.chipDescription = "Igen";
      if (x.isChipped == false) this.chipDescription = "Nem";
      if (x.animalImagesId.length != 0)
        this.loadAnimalImages(x.id, x.animalImagesId)
      x.userPreview.image$ = this.imageDownloadService.getUserProfileImage(x.userPreview.userName).pipe(map((y: Blob) => {
        return this.imageDownloadService.convertBlobToSafeUrl(y)
      }),
        catchError((err: HttpErrorResponse) => {
          console.log("Error: " + err)
          return of(AssetUrls.defaultUserImageUrl);
        }),
        retryWhen(error => error.pipe(delay(1000)))
      )
    }
    ),
      retryWhen(error => error.pipe(delay(1000))))
  }

  loadAnimalImages(animalId: number, imagesId: number[]): void {
    for (let i = 0; i < imagesId.length; i++) {
      const image: AnimalImage = {
        id: i + 1,
        image$: this.imageDownloadService.getAnimalImageByImageId(animalId, imagesId[i]).pipe(map((y: Blob) => {
          const safeUrl: SafeUrl = this.imageDownloadService.convertBlobToSafeUrl(y);
          if (!this.selectedImage)
            this.selectedImage = safeUrl;
          this.isBusy = false;
          return safeUrl;
        }),
          catchError((err: HttpErrorResponse) => {
            console.log(err)
            this.isBusy = false;
            return of(AssetUrls.defaultAnimalImageUrl);
          })
          , retryWhen(error => error.pipe(delay(1000)))
        )
      };
      this.animalImages.push(image)
    }
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  setSelectedImage(event: any) {
    this.selectedImage = event.currentSrc;
  }

  onSubmit() {
    const commentRequest = <AnimalCommentRequest>{};
    commentRequest.animalId = this.id;
    commentRequest.userName = this.authService.getUsername()!;
    commentRequest.comment = this.form.controls['comment'].value;

    this.animalService
      .uploadComment(commentRequest).pipe(tap(result => {
        console.log(result);
        this.commentResult = result;
        if (result.success) {
          location.reload();
        }
      }),
        catchError((err: HttpErrorResponse) => {
          console.log(err)
          const fail: APIResult = {
            success: false,
            message: 'API Error'
          }
          this.commentResult = fail;
          return of(fail);
        })
      ).subscribe();
  }

  getErrors(
    control: AbstractControl,
    displayName: string,
    customMessages: { [key: string]: string } | null = null
  ): string[] {
    const errors: string[] = [];
    Object.keys(control.errors || {}).forEach((key) => {
      switch (key) {
        case 'required':
          errors.push(`${displayName} ${customMessages?.[key] ?? " megadása szükséges."}`);
          break;
        default:
          errors.push(`${displayName} nem valid.`);
          break;
      }
    });
    return errors;
  }

}
