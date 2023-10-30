import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnimalUploadComponent } from './animal-upload.component';

describe('AnimalUploadComponent', () => {
  let component: AnimalUploadComponent;
  let fixture: ComponentFixture<AnimalUploadComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AnimalUploadComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AnimalUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
