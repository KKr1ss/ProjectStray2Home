import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SmallAnimalThumbnailComponent } from './small-animal-thumbnail.component';

describe('HomeAnimalThumbnailComponent', () => {
  let component: SmallAnimalThumbnailComponent;
  let fixture: ComponentFixture<SmallAnimalThumbnailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SmallAnimalThumbnailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SmallAnimalThumbnailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
