import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MidfieldersComponent } from './midfielders.component';

describe('MidfieldersComponent', () => {
  let component: MidfieldersComponent;
  let fixture: ComponentFixture<MidfieldersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MidfieldersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MidfieldersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
