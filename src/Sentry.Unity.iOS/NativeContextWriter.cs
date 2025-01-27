using System;
using System.IO;
using System.Runtime.InteropServices;
using Sentry.Extensibility;
using Sentry.Unity.Integrations;

namespace Sentry.Unity.iOS
{
    internal class NativeContextWriter : ContextWriter
    {
        protected override void WriteScope(
            string? AppStartTime,
            string? AppBuildType,
            string? OperatingSystemRawDescription,
            int? DeviceProcessorCount,
            string? DeviceCpuDescription,
            string? DeviceTimezone,
            bool? DeviceSupportsVibration,
            string? DeviceName,
            bool? DeviceSimulator,
            string? DeviceDeviceUniqueIdentifier,
            string? DeviceDeviceType,
            string? DeviceModel,
            long? DeviceMemorySize,
            int? GpuId,
            string? GpuName,
            string? GpuVendorName,
            int? GpuMemorySize,
            string? GpuNpotSupport,
            string? GpuVersion,
            string? GpuApiType,
            int? GpuMaxTextureSize,
            bool? GpuSupportsDrawCallInstancing,
            bool? GpuSupportsRayTracing,
            bool? GpuSupportsComputeShaders,
            bool? GpuSupportsGeometryShaders,
            string? GpuVendorId,
            bool? GpuMultiThreadedRendering,
            string? GpuGraphicsShaderLevel,
            string? UnityInstallMode,
            string? UnityTargetFrameRate,
            string? UnityCopyTextureSupport,
            string? UnityRenderingThreadingMode
        ) => SentryNativeBridgeWriteScope(
            // // AppStartTime,
            // AppBuildType,
            // // OperatingSystemRawDescription,
            // DeviceProcessorCount ?? 0,
            // DeviceCpuDescription,
            // DeviceTimezone,
            // marshallNullableBool(DeviceSupportsVibration),
            // DeviceName,
            // marshallNullableBool(DeviceSimulator),
            // DeviceDeviceUniqueIdentifier,
            // DeviceDeviceType,
            // // DeviceModel,
            // // DeviceMemorySize,
            GpuId ?? 0,
            GpuName,
            GpuVendorName,
            GpuMemorySize ?? 0,
            GpuNpotSupport,
            GpuVersion,
            GpuApiType,
            GpuMaxTextureSize ?? 0,
            marshallNullableBool(GpuSupportsDrawCallInstancing),
            marshallNullableBool(GpuSupportsRayTracing),
            marshallNullableBool(GpuSupportsComputeShaders),
            marshallNullableBool(GpuSupportsGeometryShaders),
            GpuVendorId,
            marshallNullableBool(GpuMultiThreadedRendering),
            GpuGraphicsShaderLevel,
            UnityInstallMode,
            UnityTargetFrameRate,
            UnityCopyTextureSupport,
            UnityRenderingThreadingMode
        );

        private static sbyte marshallNullableBool(bool? value) => (sbyte)(value.HasValue ? (value.Value ? 1 : 0) : -1);

        // Note: we only forward information that's missing or significantly different in cocoa SDK events.
        // Additionally, there's currently no way to update existing contexts, so no more Device info for now...
        [DllImport("__Internal")]
        private static extern void SentryNativeBridgeWriteScope(
            // // string? AppStartTime,
            // string? AppBuildType,
            // // string? OperatingSystemRawDescription,
            // int DeviceProcessorCount,
            // string? DeviceCpuDescription,
            // string? DeviceTimezone,
            // sbyte DeviceSupportsVibration,
            // string? DeviceName,
            // sbyte DeviceSimulator,
            // string? DeviceDeviceUniqueIdentifier,
            // string? DeviceDeviceType,
            // // string? DeviceModel,
            // // long? DeviceMemorySize,
            int GpuId,
            string? GpuName,
            string? GpuVendorName,
            int GpuMemorySize,
            string? GpuNpotSupport,
            string? GpuVersion,
            string? GpuApiType,
            int GpuMaxTextureSize,
            sbyte GpuSupportsDrawCallInstancing,
            sbyte GpuSupportsRayTracing,
            sbyte GpuSupportsComputeShaders,
            sbyte GpuSupportsGeometryShaders,
            string? GpuVendorId,
            sbyte GpuMultiThreadedRendering,
            string? GpuGraphicsShaderLevel,
            string? UnityInstallMode,
            string? UnityTargetFrameRate,
            string? UnityCopyTextureSupport,
            string? UnityRenderingThreadingMode
        );
    }
}
