import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Pipe({ name: 'blobToSafeURLConverter' })
export class BlobToSafeURLConverterPipe implements PipeTransform {
  constructor(protected sanitizer: DomSanitizer) { }
  transform(blob: Blob): SafeUrl {
    const objectURL = URL.createObjectURL(blob);
    const img = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    return img;
  }
}
