import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { catchError, map, Observable, of, tap } from 'rxjs';
import { ImageDownloadService } from '../../common/image-download.service';
import { AssetUrls } from '../../shared/asset-urls';
import { UserProfile } from '../common/models/user-profile';
import { UserService } from '../common/user.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {
  account$?: Observable<UserProfile>

  constructor(
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private imageDownloadService: ImageDownloadService) {
  }

  ngOnInit(): void {
    this.account$ = this.userService.getAccountDetails().pipe(tap(x =>
      x.image$ = this.imageDownloadService.getUserProfileImage(x.userName).pipe(map(y => {
        return this.imageDownloadService.convertBlobToSafeUrl(y)
      }),
        catchError((err: HttpErrorResponse) => {
          console.log(err);
          return of(AssetUrls.defaultUserImageUrl);
        })
      )
    ))
  }
}
