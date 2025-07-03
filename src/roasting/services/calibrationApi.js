import api from '../../shared/config/api.js';

/**
 * Servicio de API para la gestión de calibraciones
 * 
 * Este servicio proporciona métodos para interactuar con el backend
 * y gestionar las calibraciones de equipos en el sistema CafeLab.
 * 
 * Características implementadas:
 * - CRUD completo de calibraciones
 * - Filtrado por estado y tipo
 * - Consultas de calibraciones próximas a vencer
 * - Estadísticas de calibraciones
 * - Manejo de errores automático
 * - Autenticación JWT automática
 */

/**
 * Obtener todas las calibraciones del sistema
 * 
 * @returns {Promise<Array>} Lista de calibraciones
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCalibrations() {
  try {
    console.log('🔍 Obteniendo lista de calibraciones...');
    const response = await api.get('/calibrations');
    console.log('✅ Calibraciones obtenidas exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener calibraciones:', error);
    throw error;
  }
}

/**
 * Obtener una calibración específica por su ID
 * 
 * @param {number} id - ID de la calibración a obtener
 * @returns {Promise<Object>} Datos de la calibración
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCalibrationById(id) {
  try {
    console.log(`🔍 Obteniendo calibración con ID: ${id}`);
    const response = await api.get(`/calibrations/${id}`);
    console.log('✅ Calibración obtenida exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al obtener calibración ${id}:`, error);
    throw error;
  }
}

/**
 * Crear una nueva calibración
 * 
 * @param {Object} calibrationData - Datos de la calibración a crear
 * @param {string} calibrationData.name - Nombre de la calibración
 * @param {string} calibrationData.description - Descripción de la calibración
 * @param {Date} calibrationData.calibrationDate - Fecha de calibración
 * @param {string} calibrationData.result - Resultado de la calibración
 * @param {string} calibrationData.type - Tipo de calibración (Equipo/Proceso/Método)
 * @param {string} calibrationData.notes - Notas adicionales
 * @returns {Promise<Object>} Calibración creada
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function createCalibration(calibrationData) {
  try {
    console.log('➕ Creando nueva calibración:', calibrationData);
    const response = await api.post('/calibrations', calibrationData);
    console.log('✅ Calibración creada exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al crear calibración:', error);
    throw error;
  }
}

/**
 * Actualizar una calibración existente
 * 
 * @param {number} id - ID de la calibración a actualizar
 * @param {Object} calibrationData - Nuevos datos de la calibración
 * @returns {Promise<Object>} Calibración actualizada
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function updateCalibration(id, calibrationData) {
  try {
    console.log(`✏️ Actualizando calibración ${id}:`, calibrationData);
    const response = await api.put(`/calibrations/${id}`, calibrationData);
    console.log('✅ Calibración actualizada exitosamente:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al actualizar calibración ${id}:`, error);
    throw error;
  }
}

/**
 * Eliminar una calibración
 * 
 * @param {number} id - ID de la calibración a eliminar
 * @returns {Promise<Object>} Respuesta de confirmación
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function deleteCalibration(id) {
  try {
    console.log(`🗑️ Eliminando calibración ${id}`);
    const response = await api.delete(`/calibrations/${id}`);
    console.log('✅ Calibración eliminada exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al eliminar calibración ${id}:`, error);
    throw error;
  }
}

/**
 * Obtener calibraciones por estado
 * 
 * @param {string} status - Estado a filtrar (Pendiente/En Proceso/Completada/Fallida)
 * @returns {Promise<Array>} Lista de calibraciones filtradas
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCalibrationsByStatus(status) {
  try {
    console.log(`🔍 Obteniendo calibraciones por estado: ${status}`);
    const response = await api.get(`/calibrations/status/${status}`);
    console.log('✅ Calibraciones por estado obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al obtener calibraciones por estado ${status}:`, error);
    throw error;
  }
}

/**
 * Obtener calibraciones por tipo
 * 
 * @param {string} type - Tipo a filtrar (Equipo/Proceso/Método)
 * @returns {Promise<Array>} Lista de calibraciones filtradas
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCalibrationsByType(type) {
  try {
    console.log(`🔍 Obteniendo calibraciones por tipo: ${type}`);
    const response = await api.get(`/calibrations/type/${type}`);
    console.log('✅ Calibraciones por tipo obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al obtener calibraciones por tipo ${type}:`, error);
    throw error;
  }
}

/**
 * Obtener calibraciones próximas a vencer
 * 
 * @param {number} daysThreshold - Días de anticipación (por defecto 30)
 * @returns {Promise<Array>} Lista de calibraciones próximas a vencer
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getUpcomingExpirations(daysThreshold = 30) {
  try {
    console.log(`⏰ Obteniendo calibraciones próximas a vencer (${daysThreshold} días)...`);
    const response = await api.get(`/calibrations/upcoming-expirations?days=${daysThreshold}`);
    console.log('✅ Calibraciones próximas a vencer obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener calibraciones próximas a vencer:', error);
    throw error;
  }
}

/**
 * Obtener calibraciones vencidas
 * 
 * @returns {Promise<Array>} Lista de calibraciones vencidas
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getExpiredCalibrations() {
  try {
    console.log('⚠️ Obteniendo calibraciones vencidas...');
    const response = await api.get('/calibrations/expired');
    console.log('✅ Calibraciones vencidas obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener calibraciones vencidas:', error);
    throw error;
  }
}

/**
 * Obtener calibraciones por rango de fechas
 * 
 * @param {Date} startDate - Fecha de inicio
 * @param {Date} endDate - Fecha de fin
 * @returns {Promise<Array>} Lista de calibraciones en el rango
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCalibrationsByDateRange(startDate, endDate) {
  try {
    console.log(`📅 Obteniendo calibraciones entre ${startDate} y ${endDate}...`);
    const response = await api.get(`/calibrations/date-range?startDate=${startDate}&endDate=${endDate}`);
    console.log('✅ Calibraciones por rango de fechas obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener calibraciones por rango de fechas:', error);
    throw error;
  }
}

/**
 * Obtener estadísticas de calibraciones
 * 
 * @returns {Promise<Object>} Estadísticas agregadas de calibraciones
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function getCalibrationStatistics() {
  try {
    console.log('📊 Obteniendo estadísticas de calibraciones...');
    const response = await api.get('/calibrations/statistics');
    console.log('✅ Estadísticas de calibraciones obtenidas:', response.data);
    return response.data;
  } catch (error) {
    console.error('❌ Error al obtener estadísticas de calibraciones:', error);
    throw error;
  }
}

/**
 * Buscar calibraciones por nombre
 * 
 * @param {string} name - Nombre o parte del nombre a buscar
 * @returns {Promise<Array>} Lista de calibraciones que coinciden
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function searchCalibrationsByName(name) {
  try {
    console.log(`🔍 Buscando calibraciones con nombre: ${name}`);
    const response = await api.get(`/calibrations/search?name=${encodeURIComponent(name)}`);
    console.log('✅ Calibraciones encontradas:', response.data);
    return response.data;
  } catch (error) {
    console.error(`❌ Error al buscar calibraciones por nombre ${name}:`, error);
    throw error;
  }
}

/**
 * Actualizar el estado de una calibración
 * 
 * @param {number} id - ID de la calibración
 * @param {string} status - Nuevo estado (Pendiente/En Proceso/Completada/Fallida)
 * @returns {Promise<Object>} Calibración actualizada
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function updateCalibrationStatus(id, status) {
  try {
    console.log(`🔄 Actualizando estado de calibración ${id} a: ${status}`);
    const response = await api.patch(`/calibrations/${id}/status`, { status });
    console.log('✅ Estado de calibración actualizado exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al actualizar estado de calibración ${id}:`, error);
    throw error;
  }
}

/**
 * Activar una calibración (cambiar estado a activo)
 * 
 * @param {number} id - ID de la calibración a activar
 * @returns {Promise<Object>} Calibración activada
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function activateCalibration(id) {
  try {
    console.log(`✅ Activando calibración ${id}`);
    const response = await api.patch(`/calibrations/${id}/activate`);
    console.log('✅ Calibración activada exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al activar calibración ${id}:`, error);
    throw error;
  }
}

/**
 * Desactivar una calibración (cambiar estado a inactivo)
 * 
 * @param {number} id - ID de la calibración a desactivar
 * @returns {Promise<Object>} Calibración desactivada
 * @throws {Error} Si hay un error en la petición HTTP
 */
export async function deactivateCalibration(id) {
  try {
    console.log(`❌ Desactivando calibración ${id}`);
    const response = await api.patch(`/calibrations/${id}/deactivate`);
    console.log('✅ Calibración desactivada exitosamente');
    return response.data;
  } catch (error) {
    console.error(`❌ Error al desactivar calibración ${id}:`, error);
    throw error;
  }
} 