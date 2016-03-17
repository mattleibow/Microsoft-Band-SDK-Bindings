//Copyright (c) Microsoft Corporation All rights reserved.  
// 
//MIT License: 
// 
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
//documentation files (the  "Software"), to deal in the Software without restriction, including without limitation
//the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
//to permit persons to whom the Software is furnished to do so, subject to the following conditions: 
// 
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of
//the Software. 
// 
//THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
//TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
//THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
//CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
//IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using Genetics;
using Genetics.Attributes;
using Fragment = Android.Support.V4.App.Fragment;

using Microsoft.Band.Sensors;

namespace Microsoft.Band.Sample
{
    public class SensorsFragment : Fragment, FragmentListener
    {
        // Accelerometer controls
        [Splice(Resource.Id.switchAccelerometer)] private Switch mSwitchAccelerometer;
        [Splice(Resource.Id.tableAccelerometer)] private TableLayout mTableAccelerometer;
        [Splice(Resource.Id.rgAccelerometer)] private RadioGroup mRadioGroupAccelerometer;
        [Splice(Resource.Id.textAccX)] private TextView mTextAccX;
        [Splice(Resource.Id.textAccY)] private TextView mTextAccY;
        [Splice(Resource.Id.textAccZ)] private TextView mTextAccZ;
        [Splice(Resource.Id.rbAccelerometerRate16ms)] private RadioButton mRadioAcc16;
        [Splice(Resource.Id.rbAccelerometerRate32ms)] private RadioButton mRadioAcc32;
        [Splice(Resource.Id.rbAccelerometerRate128ms)] private RadioButton mRadioAcc128;

        // Gyroscope controls
        [Splice(Resource.Id.switchGyro)] private Switch mSwitchGyro;
        [Splice(Resource.Id.tableGyro)] private TableLayout mTableGyro;
        [Splice(Resource.Id.rgGyro)] private RadioGroup mRadioGroupGyro;
        [Splice(Resource.Id.textGyroAccX)] private TextView mTextGyroAccX;
        [Splice(Resource.Id.textGyroAccY)] private TextView mTextGyroAccY;
        [Splice(Resource.Id.textGyroAccZ)] private TextView mTextGyroAccZ;
        [Splice(Resource.Id.textAngX)] private TextView mTextGyroAngX;
        [Splice(Resource.Id.textAngY)] private TextView mTextGyroAngY;
        [Splice(Resource.Id.textAngZ)] private TextView mTextGyroAngZ;
        [Splice(Resource.Id.rbGyroRate16ms)] private RadioButton mRadioGyro16;
        [Splice(Resource.Id.rbGyroRate32ms)] private RadioButton mRadioGyro32;
        [Splice(Resource.Id.rbGyroRate128ms)] private RadioButton mRadioGyro128;

        // Distance sensor controls
        [Splice(Resource.Id.switchDistance)] private Switch mSwitchDistance;
        [Splice(Resource.Id.tableDistance)] private TableLayout mTableDistance;
        [Splice(Resource.Id.textTotalDistance)] private TextView mTextTotalDistance;
        [Splice(Resource.Id.textSpeed)] private TextView mTextSpeed;
        [Splice(Resource.Id.textPace)] private TextView mTextPace;
        [Splice(Resource.Id.textPedometerMode)] private TextView mTextPedometerMode;

        // HR sensor controls
        [Splice(Resource.Id.switchHeartRate)] private Switch mSwitchHeartRate;
        [Splice(Resource.Id.tableHeartRate)] private TableLayout mTableHeartRate;
        [Splice(Resource.Id.textHeartRate)] private TextView mTextHeartRate;
        [Splice(Resource.Id.textHeartRateQuality)] private TextView mTextHeartRateQuality;

        // Contact sensor controls
        [Splice(Resource.Id.switchContact)] private Switch mSwitchContact;
        [Splice(Resource.Id.tableContact)] private TableLayout mTableContact;
        [Splice(Resource.Id.textContact)] private TextView mTextContact;

        // Skin temperature sensor controls
        [Splice(Resource.Id.switchSkinTemperature)] private Switch mSwitchSkinTemperature;
        [Splice(Resource.Id.tableSkinTemperature)] private TableLayout mTableSkinTemperature;
        [Splice(Resource.Id.textSkinTemperature)] private TextView mTextSkinTemperature;

        // UV sensor controls
        [Splice(Resource.Id.switchUltraviolet)] private Switch mSwitchUltraviolet;
        [Splice(Resource.Id.tableUltraviolet)] private TableLayout mTableUltraviolet;
        [Splice(Resource.Id.textUltraviolet)] private TextView mTextUltraviolet;

        // Pedometer sensor controls
        [Splice(Resource.Id.switchPedometer)] private Switch mSwitchPedometer;
        [Splice(Resource.Id.tablePedometer)] private TableLayout mTablePedometer;
        [Splice(Resource.Id.textTotalSteps)] private TextView mTextTotalSteps;

        // Altimeter sensor controls
        [Splice(Resource.Id.switchAltimeter)] private Switch mSwitchAltimeter;
        [Splice(Resource.Id.tableAltimeter)] private TableLayout mTableAltimeter;
        [Splice(Resource.Id.textFlightsAscended)] private TextView mTextFlightsAscended;
        [Splice(Resource.Id.textFlightsDescended)] private TextView mTextFlightsDescended;
        [Splice(Resource.Id.textRate)] private TextView mTextRate;
        [Splice(Resource.Id.textSteppingGain)] private TextView mTextSteppingGain;
        [Splice(Resource.Id.textSteppingLoss)] private TextView mTextSteppingLoss;
        [Splice(Resource.Id.textStepsAscended)] private TextView mTextStepsAscended;
        [Splice(Resource.Id.textStepsDescended)] private TextView mTextStepsDescended;
        [Splice(Resource.Id.textTotalGain)] private TextView mTextTotalGain;
        [Splice(Resource.Id.textTotalLoss)] private TextView mTextTotalLoss;

        // Ambient light sensor controls
        [Splice(Resource.Id.switchAmbientLight)] private Switch mSwitchAmbientLight;
        [Splice(Resource.Id.tableAmbientLight)] private TableLayout mTableAmbientLight;
        [Splice(Resource.Id.textBrightness)] private TextView mTextBrightness;

        // Barometer sensor controls
        [Splice(Resource.Id.switchBarometer)] private Switch mSwitchBarometer;
        [Splice(Resource.Id.tableBarometer)] private TableLayout mTableBarometer;
        [Splice(Resource.Id.textAirPressure)] private TextView mTextAirPressure;
        [Splice(Resource.Id.textTemperature)] private TextView mTextTemperature;

        // Calories sensor controls
        [Splice(Resource.Id.switchCalories)] private Switch mSwitchCalories;
        [Splice(Resource.Id.tableCalories)] private TableLayout mTableCalories;
        [Splice(Resource.Id.textCalories)] private TextView mTextCalories;

        // Gsr sensor controls
        [Splice(Resource.Id.switchGsr)] private Switch mSwitchGsr;
        [Splice(Resource.Id.tableGsr)] private TableLayout mTableGsr;
        [Splice(Resource.Id.textResistance)] private TextView mTextResistance;

        // Rr interval sensor controls
        [Splice(Resource.Id.switchRRInterval)] private Switch mSwitchRRInterval;
        [Splice(Resource.Id.tableRRInterval)] private TableLayout mTableRRInterval;
        [Splice(Resource.Id.textInterval)] private TextView mTextInterval;

        // Each sensor switch has an associated TableLayout containing it's display controls.
        // The TableLayout remains hidden until the corresponding sensor switch is turned on.
        private Dictionary<Switch, TableLayout> mSensorMap;

        // the sensors
        private AccelerometerSensor accelerometerSensor;
        private ContactSensor contactSensor;
        private DistanceSensor distanceSensor;
        private GyroscopeSensor gyroscopeSensor;
        private HeartRateSensor heartRateSensor;
        private PedometerSensor pedometerSensor;
        private SkinTemperatureSensor skinTemperatureSensor;
        private UVSensor uvSensor;
        private AltimeterSensor altimeterSensor;
        private AmbientLightSensor ambientLightSensor;
        private BarometerSensor barometerSensor;
        private CaloriesSensor caloriesSensor;
        private GsrSensor gsrSensor;
        private RRIntervalSensor rrIntervalSensor;

        public virtual void OnFragmentSelected()
        {
            if (IsVisible)
            {
                RefreshControls();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View rootView = inflater.Inflate(Resource.Layout.fragment_sensors, container, false);

            Geneticist.Splice(this, rootView);

            mSensorMap = new Dictionary<Switch, TableLayout>();

            // Accelerometer setup
            mSensorMap[mSwitchAccelerometer] = mTableAccelerometer;
            mTableAccelerometer.Visibility = ViewStates.Gone;

            // Gyro setup
            mSensorMap[mSwitchGyro] = mTableGyro;
            mTableGyro.Visibility = ViewStates.Gone;

            // Distance setup
            mSensorMap[mSwitchDistance] = mTableDistance;
            mTableDistance.Visibility = ViewStates.Gone;

            // Heart rate setup
            mSensorMap[mSwitchHeartRate] = mTableHeartRate;
            mTableHeartRate.Visibility = ViewStates.Gone;

            // Contact setup
            mSensorMap[mSwitchContact] = mTableContact;
            mTableContact.Visibility = ViewStates.Gone;

            // Skin temperature setup
            mSensorMap[mSwitchSkinTemperature] = mTableSkinTemperature;
            mTableSkinTemperature.Visibility = ViewStates.Gone;

            // Ultraviolet setup
            mSensorMap[mSwitchUltraviolet] = mTableUltraviolet;
            mTableUltraviolet.Visibility = ViewStates.Gone;

            // Pedometer setup
            mSensorMap[mSwitchPedometer] = mTablePedometer;
            mTablePedometer.Visibility = ViewStates.Gone;

            // Altimeter setup
            mSensorMap[mSwitchAltimeter] = mTableAltimeter;
            mTableAltimeter.Visibility = ViewStates.Gone;

            // Ambient light rate setup
            mSensorMap[mSwitchAmbientLight] = mTableAmbientLight;
            mTableAmbientLight.Visibility = ViewStates.Gone;

            // Barometer setup
            mSensorMap[mSwitchBarometer] = mTableBarometer;
            mTableBarometer.Visibility = ViewStates.Gone;

            // Calories setup
            mSensorMap[mSwitchCalories] = mTableCalories;
            mTableCalories.Visibility = ViewStates.Gone;

            // Gsr setup
            mSensorMap[mSwitchGsr] = mTableGsr;
            mTableGsr.Visibility = ViewStates.Gone;

            // Rr interval setup
            mSensorMap[mSwitchRRInterval] = mTableRRInterval;
            mTableRRInterval.Visibility = ViewStates.Gone;

            return rootView;
        }


        //
        // Sensor event handlers
        //
        private void EnsureSensorsCreated()
        {
            IBandSensorManager sensorMgr = Model.Instance.Client.SensorManager;

            if (accelerometerSensor == null)
            {
                accelerometerSensor = sensorMgr.CreateAccelerometerSensor();
                accelerometerSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var accelerometerEvent = e.SensorReading;
                        mTextAccX.Text = string.Format("{0:F3} m/s/s", accelerometerEvent.AccelerationX);
                        mTextAccY.Text = string.Format("{0:F3} m/s/s", accelerometerEvent.AccelerationY);
                        mTextAccZ.Text = string.Format("{0:F3} m/s/s", accelerometerEvent.AccelerationZ);
                    });
                };
            }

            if (contactSensor == null)
            {
                contactSensor = sensorMgr.CreateContactSensor();
                contactSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var contactEvent = e.SensorReading;
                        mTextContact.Text = contactEvent.ContactState.ToString();
                    });
                };
            }

            if (distanceSensor == null)
            {
                distanceSensor = sensorMgr.CreateDistanceSensor();
                distanceSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var distanceEvent = e.SensorReading;
                        mTextTotalDistance.Text = string.Format("{0:D} cm", distanceEvent.TotalDistance);
                        mTextSpeed.Text = string.Format("{0:F2} cm/s", distanceEvent.Speed);
                        mTextPace.Text = string.Format("{0:F2} ms/m", distanceEvent.Pace);
                        mTextPedometerMode.Text = distanceEvent.MotionType.ToString();
                    });
                };
            }

            if (gyroscopeSensor == null)
            {
                gyroscopeSensor = sensorMgr.CreateGyroscopeSensor();
                gyroscopeSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var gyroscopeEvent = e.SensorReading;
                        mTextGyroAccX.Text = string.Format("{0:F3} m/s/s", gyroscopeEvent.AccelerationX);
                        mTextGyroAccY.Text = string.Format("{0:F3} m/s/s", gyroscopeEvent.AccelerationY);
                        mTextGyroAccZ.Text = string.Format("{0:F3} m/s/s", gyroscopeEvent.AccelerationZ);
                        mTextGyroAngX.Text = string.Format("{0:F2} deg/s", gyroscopeEvent.AngularVelocityX);
                        mTextGyroAngY.Text = string.Format("{0:F2} deg/s", gyroscopeEvent.AngularVelocityY);
                        mTextGyroAngZ.Text = string.Format("{0:F2} deg/s", gyroscopeEvent.AngularVelocityZ);
                    });
                };
            }

            if (heartRateSensor == null)
            {
                heartRateSensor = sensorMgr.CreateHeartRateSensor();
                heartRateSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var heartRateEvent = e.SensorReading;
                        mTextHeartRate.Text = string.Format("{0:D} beats/min", heartRateEvent.HeartRate);
                        mTextHeartRateQuality.Text = heartRateEvent.Quality.ToString();
                    });
                };
            }

            if (pedometerSensor == null)
            {
                pedometerSensor = sensorMgr.CreatePedometerSensor();
                pedometerSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var pedometerEvent = e.SensorReading;
                        mTextTotalSteps.Text = string.Format("{0:D} steps", pedometerEvent.TotalSteps);
                    });
                };
            }

            if (skinTemperatureSensor == null)
            {
                skinTemperatureSensor = sensorMgr.CreateSkinTemperatureSensor();
                skinTemperatureSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var skinTemperatureEvent = e.SensorReading;
                        mTextSkinTemperature.Text = string.Format("{0:F1} (C)", skinTemperatureEvent.Temperature);
                    });
                };
            }

            if (uvSensor == null)
            {
                uvSensor = sensorMgr.CreateUVSensor();
                uvSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var uvEvent = e.SensorReading;
                        mTextUltraviolet.Text = uvEvent.UVIndexLevel.ToString();
                    });
                };
            }
            
            if (altimeterSensor == null)
            {
                altimeterSensor = sensorMgr.CreateAltimeterSensor();
                altimeterSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var altimeterEvent = e.SensorReading;
                        mTextFlightsAscended.Text = string.Format("{0:D} floors", altimeterEvent.FlightsAscended);
                        mTextFlightsDescended.Text = string.Format("{0:D} floors", altimeterEvent.FlightsDescended);
                        mTextRate.Text = string.Format("{0:F2} cm/s", altimeterEvent.Rate);
                        mTextSteppingGain.Text = string.Format("{0:D} cm", altimeterEvent.SteppingGain);
                        mTextSteppingLoss.Text = string.Format("{0:D} cm", altimeterEvent.SteppingLoss);
                        mTextStepsAscended.Text = string.Format("{0:D} steps", altimeterEvent.StepsAscended);
                        mTextStepsDescended.Text = string.Format("{0:D} steps", altimeterEvent.StepsDescended);
                        mTextTotalGain.Text = string.Format("{0:D} cm", altimeterEvent.TotalGain);
                        mTextTotalLoss.Text = string.Format("{0:D} cm", altimeterEvent.TotalLoss);
                    });
                };
            }

            if (ambientLightSensor == null)
            {
                ambientLightSensor = sensorMgr.CreateAmbientLightSensor();
                ambientLightSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var ambientLightEvent = e.SensorReading;
                        mTextBrightness.Text = string.Format("{0:D} lux", ambientLightEvent.Brightness);
                    });
                };
            }
            
            if (barometerSensor == null)
            {
                barometerSensor = sensorMgr.CreateBarometerSensor();
                barometerSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var barometerEvent = e.SensorReading;
                        mTextAirPressure.Text = string.Format("{0:F2} hPa", barometerEvent.AirPressure);
                        mTextTemperature.Text = string.Format("{0:F2} (C)", barometerEvent.Temperature);
                    });
                };
            }
            
            if (caloriesSensor == null)
            {
                caloriesSensor = sensorMgr.CreateCaloriesSensor();
                caloriesSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var caloriesEvent = e.SensorReading;
                        mTextCalories.Text = string.Format("{0:D} kcals", caloriesEvent.Calories);
                    });
                };
            }
            
            if (gsrSensor == null)
            {
                gsrSensor = sensorMgr.CreateGsrSensor();
                gsrSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var gsrEvent = e.SensorReading;
                        mTextResistance.Text = string.Format("{0:D} kohms", gsrEvent.Resistance);
                    });
                };
            }
            
            if (rrIntervalSensor == null)
            {
                rrIntervalSensor = sensorMgr.CreateRRIntervalSensor();
                rrIntervalSensor.ReadingChanged += (sender, e) =>
                {
                    Activity.RunOnUiThread(() =>
                    {
                        var rrIntervalEvent = e.SensorReading;
                        mTextInterval.Text = string.Format("{0:2F} s", rrIntervalEvent.Interval);
                    });
                };
            }
        }


        //
        // When pausing, turn off any active sensors.
        //
        public override void OnPause()
        {
            foreach (Switch sw in mSensorMap.Keys)
            {
                if (sw.Checked)
                {
                    sw.Checked = false;
                    OnToggleSensorSection(sw, new CompoundButton.CheckedChangeEventArgs(false));
                }
            }

            base.OnPause();
        }

        [SpliceCheckedChange(Resource.Id.switchAccelerometer)]
        [SpliceCheckedChange(Resource.Id.switchContact)]
        [SpliceCheckedChange(Resource.Id.switchDistance)]
        [SpliceCheckedChange(Resource.Id.switchGyro)]
        [SpliceCheckedChange(Resource.Id.switchHeartRate)]
        [SpliceCheckedChange(Resource.Id.switchPedometer)]
        [SpliceCheckedChange(Resource.Id.switchSkinTemperature)]
        [SpliceCheckedChange(Resource.Id.switchUltraviolet)]
        [SpliceCheckedChange(Resource.Id.switchAltimeter)]
        [SpliceCheckedChange(Resource.Id.switchAmbientLight)]
        [SpliceCheckedChange(Resource.Id.switchBarometer)]
        [SpliceCheckedChange(Resource.Id.switchCalories)]
        [SpliceCheckedChange(Resource.Id.switchGsr)]
        [SpliceCheckedChange(Resource.Id.switchRRInterval)]
        private async void OnToggleSensorSection(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (!Model.Instance.Connected)
            {
                return;
            }

            EnsureSensorsCreated();

            Switch sw = (Switch)sender;
            TableLayout table = mSensorMap[sw];

            if (e.IsChecked)
            {
                table.Visibility = ViewStates.Visible;

                if (table == mTableAccelerometer)
                {
                    mRadioGroupAccelerometer.Enabled = false;
                    SetChildrenEnabled(mRadioGroupAccelerometer, false);
                }
                else if (table == mTableGyro)
                {
                    mRadioGroupGyro.Enabled = false;
                    SetChildrenEnabled(mRadioGroupGyro, false);
                }

                // Turn on the appropriate sensor

                try
                {
                    if (sw == mSwitchAccelerometer)
                    {
                        SampleRate rate;
                        if (mRadioAcc16.Checked)
                        {
                            rate = SampleRate.Ms16;
                        }
                        else if (mRadioAcc32.Checked)
                        {
                            rate = SampleRate.Ms32;
                        }
                        else
                        {
                            rate = SampleRate.Ms128;
                        }

                        mTextAccX.Text = "";
                        mTextAccY.Text = "";
                        mTextAccZ.Text = "";
                        accelerometerSensor.StartReadings(rate);
                    }
                    else if (sw == mSwitchGyro)
                    {
                        SampleRate rate;
                        if (mRadioGyro16.Checked)
                        {
                            rate = SampleRate.Ms16;
                        }
                        else if (mRadioGyro32.Checked)
                        {
                            rate = SampleRate.Ms32;
                        }
                        else
                        {
                            rate = SampleRate.Ms128;
                        }

                        mTextGyroAccX.Text = "";
                        mTextGyroAccY.Text = "";
                        mTextGyroAccZ.Text = "";
                        mTextGyroAngX.Text = "";
                        mTextGyroAngY.Text = "";
                        mTextGyroAngZ.Text = "";
						gyroscopeSensor.StartReadings(rate);
                    }
                    else if (sw == mSwitchDistance)
                    {
                        mTextTotalDistance.Text = "";
                        mTextSpeed.Text = "";
                        mTextPace.Text = "";
                        mTextPedometerMode.Text = "";
						distanceSensor.StartReadings();
                    }
                    else if (sw == mSwitchHeartRate)
                    {
						var sensorMngr = Model.Instance.Client.SensorManager;
						if (await sensorMngr.RequestHeartRateConsentTaskAsync(Activity)) 
						{
							mTextHeartRate.Text = "";
							mTextHeartRateQuality.Text = "";
							heartRateSensor.StartReadings();
						}
						else
						{
							Util.ShowExceptionAlert(Activity, "Start heart rate sensor", new Exception("User declined permission."));
                            mSwitchHeartRate.Checked = false;
						}
                    }
                    else if (sw == mSwitchContact)
                    {
                        mTextContact.Text = "";
						contactSensor.StartReadings();
                    }
                    else if (sw == mSwitchSkinTemperature)
                    {
                        mTextSkinTemperature.Text = "";
						skinTemperatureSensor.StartReadings();
                    }
                    else if (sw == mSwitchUltraviolet)
                    {
                        mTextUltraviolet.Text = "";
						uvSensor.StartReadings();
                    }
                    else if (sw == mSwitchPedometer)
                    {
                        mTextTotalSteps.Text = "";
						pedometerSensor.StartReadings();
                    }
                    else if (sw == mSwitchAltimeter)
                    {
                        mTextFlightsAscended.Text = "";
                        mTextFlightsDescended.Text = "";
                        mTextRate.Text = "";
                        mTextSteppingGain.Text = "";
                        mTextSteppingLoss.Text = "";
                        mTextStepsAscended.Text = "";
                        mTextStepsDescended.Text = "";
                        mTextTotalGain.Text = "";
                        mTextTotalLoss.Text = "";
                        altimeterSensor.StartReadings();
                    }
                    else if (sw == mSwitchAmbientLight)
                    {
                        mTextBrightness.Text = "";
                        ambientLightSensor.StartReadings();
                    }
                    else if (sw == mSwitchBarometer)
                    {
                        mTextAirPressure.Text = "";
                        mTextTemperature.Text = "";
                        barometerSensor.StartReadings();
                    }
                    else if (sw == mSwitchCalories)
                    {
                        mTextCalories.Text = "";
                        caloriesSensor.StartReadings();
                    }
                    else if (sw == mSwitchGsr)
                    {
                        mTextResistance.Text = "";
                        gsrSensor.StartReadings();
                    }
                    else if (sw == mSwitchRRInterval)
                    {
                        var sensorMngr = Model.Instance.Client.SensorManager;
                        if (await sensorMngr.RequestHeartRateConsentTaskAsync(Activity)) 
                        {
                            mTextInterval.Text = "";
                            rrIntervalSensor.StartReadings();
                        }
                        else
                        {
                            Util.ShowExceptionAlert(Activity, "Start rr interval sensor", new Exception("User declined permission."));
                            mSwitchRRInterval.Checked = false;
                        }
                    }
                }
                catch (BandException ex)
                {
                    Util.ShowExceptionAlert(Activity, "Register sensor listener", ex);
                }
            }
            else
            {
                table.Visibility = ViewStates.Gone;

                if (table == mTableAccelerometer)
                {
                    mRadioGroupAccelerometer.Enabled = true;
                    SetChildrenEnabled(mRadioGroupAccelerometer, true);
                }
                else if (table == mTableGyro)
                {
                    mRadioGroupGyro.Enabled = true;
                    SetChildrenEnabled(mRadioGroupGyro, true);
                }

                // Turn off the appropriate sensor

                try
                {
                    if (sw == mSwitchAccelerometer)
                    {
                        accelerometerSensor.StopReadings();
                    }
                    else if (sw == mSwitchGyro)
                    {
						gyroscopeSensor.StopReadings();
                    }
                    else if (sw == mSwitchDistance)
                    {
						distanceSensor.StopReadings();
                    }
                    else if (sw == mSwitchHeartRate)
                    {
						heartRateSensor.StopReadings();
                    }
                    else if (sw == mSwitchContact)
                    {
						contactSensor.StopReadings();
                    }
                    else if (sw == mSwitchSkinTemperature)
                    {
						skinTemperatureSensor.StopReadings();
                    }
                    else if (sw == mSwitchUltraviolet)
                    {
						uvSensor.StopReadings();
                    }
                    else if (sw == mSwitchPedometer)
                    {
						pedometerSensor.StopReadings();
                    }
                    else if (sw == mSwitchAltimeter)
                    {
                        altimeterSensor.StopReadings();
                    }
                    else if (sw == mSwitchAmbientLight)
                    {
                        ambientLightSensor.StopReadings();
                    }
                    else if (sw == mSwitchBarometer)
                    {
                        barometerSensor.StopReadings();
                    }
                    else if (sw == mSwitchCalories)
                    {
                        caloriesSensor.StopReadings();
                    }
                    else if (sw == mSwitchGsr)
                    {
                        gsrSensor.StopReadings();
                    }
                    else if (sw == mSwitchRRInterval)
                    {
                        rrIntervalSensor.StopReadings();
                    }
                }
                catch (BandException ex)
                {
                    Util.ShowExceptionAlert(Activity, "Unregister sensor listener", ex);
                }
            }
        }

        //
        // Other helpers
        //

        private static void SetChildrenEnabled(RadioGroup radioGroup, bool enabled)
        {
            for (int i = radioGroup.ChildCount - 1; i >= 0; i--)
            {
                radioGroup.GetChildAt(i).Enabled = enabled;
            }
        }

        private void RefreshControls()
        {
            bool connected = Model.Instance.Connected;

            foreach (Switch sw in mSensorMap.Keys)
            {
                sw.Enabled = connected;
                if (!connected)
                {
                    sw.Checked = false;
                    OnToggleSensorSection(sw, new CompoundButton.CheckedChangeEventArgs(false));
                }
            }
        }
    }

}
