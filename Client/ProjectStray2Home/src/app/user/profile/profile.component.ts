import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { catchError, map, Observable, of} from 'rxjs';
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

  constructor(
    private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private imageDownloadService: ImageDownloadService) {
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
          })
        )
        return x;
      }))
    })

    //let usernameParam = this.activatedRoute.snapshot.paramMap.get('username');
    //this.username = usernameParam!
    //this.profile$ = this.userService.getProfileDetails(this.username).pipe(map(x => {
    //  x.sex = (x.sex == "Male" ? "Férfi" : "Nő")
    //  x.image$ = this.imageDownloadService.getUserProfileImage(x.id).pipe(map(y => {
    //    return this.imageDownloadService.convertBlobToSafeUrl(y)
    //  }),
    //    catchError((err: HttpErrorResponse) => {
    //      return of(AssetUrls.defaultUserImageUrl);
    //    })
    //  )
    //  return x;
    //}))
    
  }
}
