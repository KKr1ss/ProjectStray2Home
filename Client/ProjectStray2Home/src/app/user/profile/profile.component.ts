import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { catchError, delay, map, Observable, of, retryWhen} from 'rxjs';
import { BreakpointService } from '../../common/breakpoint.service';
import { ImageDownloadService } from '../../common/image-download.service';
import { AssetUrls } from '../../shared/asset-urls';
import { UserProfile } from '../common/models/user-profile';
import { UserService } from '../common/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile$?: Observable<UserProfile>
  username!: string

  isTablet$?: Observable<boolean>

  constructor(
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private imageDownloadService: ImageDownloadService,
    private breakpointService: BreakpointService) {
  }

  ngOnInit(): void {
    this.activatedRoute.paramMap.subscribe((params: ParamMap) => {
      let usernameParam = params.get('username');
      this.username = usernameParam!
      this.profile$ = this.userService.getProfileDetails(this.username).pipe(map(x => {
        x.sex = (x.sex == "Male" ? "Férfi" : "Nő")
        x.image$ = this.imageDownloadService.getUserProfileImage(x.userName).pipe(map(y => {
          return this.imageDownloadService.convertBlobToSafeUrl(y)
        }),
          catchError((err: HttpErrorResponse) => {
            console.log(err.message);
            return of(AssetUrls.defaultUserImageUrl);
          }),
          retryWhen(error => error.pipe(delay(1000)))
        )
        return x;
      }),
        retryWhen(error => error.pipe(delay(1000))))
    })

     this.isTablet$ = this.breakpointService.isTablet();
  }
}
