using System;

using Foundation;

namespace Microsoft.Band.Sensors
{
	// base type for each sensor

	public abstract class BandSensorBase<T>
		where T : BandSensorData
	{
		protected IBandSensorManager SensorManager;

		protected BandSensorBase (IBandSensorManager sensorManager)
		{
			SensorManager = sensorManager;
		}

		public event EventHandler<BandSensorDataEventArgs<T>> ReadingChanged;

		protected virtual void OnReadingChanged (BandSensorDataEventArgs<T> e)
		{
			var handler = ReadingChanged;
			if (handler != null) {
				handler (this, e);
			}
		}

		public virtual void StartReadings ()
		{
			StartReadings (null);
		}

		public abstract void StartReadings (NSOperationQueue queue);

		public abstract void StopReadings ();
	}

	// base type for sensor events

	public abstract class BandSensorDataEventArgs<T>
        where T : BandSensorData
	{
		protected BandSensorDataEventArgs (T data, NSError error)
			: this (data, error == null ? null : new BandException (error))
		{
		}

		protected BandSensorDataEventArgs (T data, BandException exception)
		{
			SensorReading = data;
			Exception = exception;
		}

		public T SensorReading { get; protected set; }

		public BandException Exception { get; protected set; }
	}
}
