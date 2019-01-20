import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DefendersComponent } from './defenders.component';

describe('DefendersComponent', () => {
  let component: DefendersComponent;
  let fixture: ComponentFixture<DefendersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DefendersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DefendersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
