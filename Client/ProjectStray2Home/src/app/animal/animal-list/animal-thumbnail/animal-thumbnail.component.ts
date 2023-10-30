import { HttpErrorResponse } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { catchError, map, of } from 'rxjs';
import { ImageDownloadService } from '../../../common/image-download.service';
import { AssetUrls } from '../../../shared/asset-urls';
import { DaysSinceStatusChangePipe } from '../../../shared/pipes/days-since-status-change.pipe';
import { AnimalPreview } from '../../common/animal-preview';

@Component({
  selector: 'app-animal-thumbnail',
  templateUrl: './animal-thumbnail.component.html',
  styleUrls: ['./animal-thumbnail.component.scss']
})
export class AnimalThumbnailComponent implements OnInit {
  @Input() animal!: AnimalPreview
  days!: string

  constructor(
    private imageDownloadService: ImageDownloadService) { }

  ngOnInit(): void {
    const dateDiff = new DaysSinceStatusChangePipe().transform(new Date(this.animal.statusDate))
    this.days = dateDiff > 0 ? dateDiff.toString() + " napja" : "ma";
    this.animal.image$ = this.imageDownloadService.getAnimalImage(this.animal.id).pipe(map(y => {
      return this.imageDownloadService.convertBlobToSafeUrl(y);
    }),
      catchError((err: HttpErrorResponse) => {
        console.log("error:" + err)
        return of(AssetUrls.defaultAnimalImageUrl);
      })
    )
  }
}
